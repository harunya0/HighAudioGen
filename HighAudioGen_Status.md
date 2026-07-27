# HighAudioGen — 現状のファイル構成 / 実装済みメンバー

これまでの会話で共有・決定された内容をもとに整理したものです。実際のディレクトリと差分があれば教えてください。

## ファイルディレクトリ

```text
HighAudioGen
├── HighAudioGen.sln
│
├── Core                          [依存なし]
│   └── Audio
│       ├── WaveData.cs
│       ├── Peak.cs
│       ├── WavePeakCache.cs
│       ├── WavePeakCacheBuilder.cs
│       └── WaveFileReader.cs
│
├── SDK                           [→ Core]
│   ├── Graph
│   │   ├── IAudioNode.cs
│   │   ├── IAudioPort.cs
│   │   ├── AudioPort.cs
│   │   ├── AudioProcessContext.cs
│   │   └── ISeekAware.cs
│   ├── Plugins
│   │   ├── IPlugin.cs            ← 重複定義バグ + Parameters未追加（要修正）
│   │   └── PluginMetadata.cs
│   ├── Parameters
│   │   ├── PluginParameterType.cs
│   │   ├── PluginParameterDefinition.cs
│   │   ├── BooleanParameterDefinition.cs
│   │   ├── DirectoryParameterDefinition.cs
│   │   ├── DoubleParameterDefinition.cs
│   │   ├── EnumParameterDefinition.cs
│   │   ├── FileParameterDefinition.cs
│   │   ├── IntegerParameterDefinition.cs
│   │   └── StringParameterDefinition.cs
│   ├── Results
│   │   ├── ParameterValidationSeverity.cs
│   │   ├── ParameterValidationIssue.cs
│   │   └── ParameterValidationResult.cs
│   └── Validation
│       ├── IPluginParameterValidator.cs
│       ├── ValidationCodes.cs
│       └── PluginParameterValidator.cs   ← 配置済みか未確認
│
├── AudioEngine                   [→ Core, SDK]
│   └── (未着手：SourceNode / EffectNode / MixNode / SpatialNode /
│        OutputNode / Transport / グラフ実行エンジン本体)
│
├── Analysis                      [→ Core]
│   └── (未着手：FFT / Spectrogram)
│
├── Codecs                        [→ Core, SDK]
│   └── (未着手：WAV / MP3 実デコード・エンコード)
│
├── Desktop                       [→ AudioEngine, Analysis, Codecs]
│   └── (Avaloniaテンプレートのまま、未着手)
│
└── Tests                         [→ Core, SDK, AudioEngine]
    └── (未着手)
```

---

## 実装済みメンバーまとめ

### Core.Audio

| 型 | 種別 | メンバー |
|---|---|---|
| `WaveData` | sealed class | `Samples: float[]`, `SampleRate: int`, `Channels: int`, `PeakCache: WavePeakCache?`, `SampleCount: int`（算出） |
| `Peak` | readonly struct | `Peak(float min, float max)`, `Min: float`, `Max: float` |
| `WavePeakCache` | sealed class | `Peaks: Peak[]`, `SamplesPerPeak: int`, `Count: int`（算出） |
| `WavePeakCacheBuilder` | static class | `Build(WaveData wave, int samplesPerPeak): WavePeakCache` |
| `WaveFileReader` | static class | `Read(string path): WaveData`（NAudio経由。読み込み後にPeakCacheも自動構築） |

### SDK.Graph

| 型 | 種別 | メンバー |
|---|---|---|
| `IAudioNode` | interface | `Id: string`, `Inputs: IReadOnlyList<IAudioPort>`, `Outputs: IReadOnlyList<IAudioPort>`, `Process(AudioProcessContext context): void` |
| `IAudioPort` | interface | `Name: string`, `ChannelCount: int` |
| `AudioPort` | sealed record | `IAudioPort`実装（`Name`, `ChannelCount`） |
| `AudioProcessContext` | sealed class | `PlayheadSample: long`, `SampleRate: int`, `FrameCount: int` |
| `ISeekAware` | interface | `PrepareForSeek(long currentSample, long targetSample): void` |

### SDK.Plugins

| 型 | 種別 | メンバー | 備考 |
|---|---|---|---|
| `IPlugin` | interface | `IAudioNode`を継承、`Metadata: PluginMetadata` | ⚠️ 重複`IAudioNode`定義の削除、`Parameters: IReadOnlyList<PluginParameterDefinition>`の追加が未反映 |
| `PluginMetadata` | sealed record | `PluginId`, `Name`, `Summary?`, `Description?`, `Version`, `Author`, `Website?`, `License?`（すべて`string`/`Uri`） |

### SDK.Parameters

| 型 | 種別 | 主なメンバー |
|---|---|---|
| `PluginParameterType` | enum | `Integer / Double / Boolean / String / Enum / File / Directory / Color / Time / Frequency` |
| `PluginParameterDefinition` | abstract record | `Id`, `DisplayName`, `Summary?`, `Description?`, `Group?`, `Order`, `Type`（抽象） |
| `DoubleParameterDefinition` | sealed record | `DefaultValue`, `Minimum?`, `Maximum?`, `RecommendedMinimum?`, `RecommendedMaximum?`, `Step?`, `Unit?` |
| `IntegerParameterDefinition` | sealed record | `DoubleParameterDefinition`と同構成（`int`版） |
| `BooleanParameterDefinition` | sealed record | `DefaultValue: bool` |
| `StringParameterDefinition` | sealed record | `DefaultValue`, `MinimumLength?`, `MaximumLength?` |
| `FileParameterDefinition` | sealed record | `DefaultValue`, `MustExist: bool` |
| `DirectoryParameterDefinition` | sealed record | `DefaultValue`, `MustExist: bool` |
| `EnumParameterDefinition<TEnum>` | sealed record | `DefaultValue: TEnum` |

### SDK.Results

| 型 | 種別 | メンバー |
|---|---|---|
| `ParameterValidationSeverity` | enum | `Info / Warning / Error` |
| `ParameterValidationIssue` | sealed record | `Code`, `ParameterId?`, `Severity`, `Message` |
| `ParameterValidationResult` | sealed record | `Issues: IReadOnlyList<ParameterValidationIssue>`, `IsValid`（算出）, `HasWarnings`（算出） |

### SDK.Validation

| 型 | 種別 | メンバー |
|---|---|---|
| `IPluginParameterValidator` | interface | `Validate(...)`のオーバーロード群（Double/Integer/Boolean/String/File/Directory/Enum<T>それぞれに対応） |
| `ValidationCodes` | static class | `BelowMinimum`, `AboveMaximum`, `BelowRecommendedMinimum`, `AboveRecommendedMaximum`, `FileNotFound`, `DirectoryNotFound`, `InvalidEnumValue`（すべて`const string`） |
| `PluginParameterValidator` | sealed class | `IPluginParameterValidator`の実装本体（各`Validate`オーバーロードの中身あり） |

---

## 未実装（設計は決定済み）

- **AudioEngine**：ノードグラフ実行エンジン、`SourceNode`/`EffectNode`/`MixNode`/`SpatialNode`/`OutputNode`、`Transport`
- **Codecs**：WAV/MP3の実デコード・エンコード
- **Analysis**：FFT/Spectrogram
- **Desktop**：UI本体
- **Tests**：一式
