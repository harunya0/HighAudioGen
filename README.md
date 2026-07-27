# HighAudioGen

**Audio IDE** — 音を解析・設計・開発・可視化・拡張するための統合開発環境。

DAW（音楽制作ソフト）の代替ではありません。一般的なDAWが隠してしまう情報も、必要ならすべて閲覧・編集できることを目指すプロジェクトです。

> Understand Audio. Inspect Audio. Develop Audio.

---

## Vision

音を編集するだけではなく、

- 音を解析する
- 音を設計する
- 音を開発する
- 音を可視化する
- 音を拡張する

ための統合開発環境を目標としています。

## Philosophy

初心者向けソフトではありません。基本はシンプルに、必要ならどこまでも深く。

Visual Studioのように、**通常モード → Developer Mode → Internal Mode** を段階的に開放できる設計を採用します。Internal ModeではAudio Engine内部まで編集可能にします。

## Design Policy

- **Engine First** — UIよりAudio Engineを優先。DesktopはEngineのクライアントの一つに過ぎず、Audio Engineがプロジェクトの本体
- **UIとEngineを完全分離** — UIは描画のみを担当し、編集・解析・計算はすべてEngineが行う。将来的にDesktop / CLI / Web / Pluginなど複数フロントエンドから利用できる設計を目指す
- **Plugin First** — ビルトイン機能も外部プラグインも同じインターフェース（`IPlugin` / `IAudioNode`）を実装する。特別扱いされる機能は存在しない（Track/Busも「複数入力を受けるEffectNode」として実装）
- **Core is Independent** — CoreはUIに依存せず、Audio EngineはAvaloniaを参照しない
- **疎結合** — プラグイン・UI・Coreは互いの実装を知らない。例えばシーク時の内部状態復元は、Engineがプラグインの中身を知ることなく`ISeekAware`のような任意実装インターフェース経由で行う

## Architecture概要

Timelineは**UI側の責務**であり、AudioEngineの機能範囲には含みません。Engineはその代わりに、音の配置と重ね合わせを支える**ノードグラフ**（`SourceNode` / `EffectNode` / `MixNode` / `SpatialNode` / `OutputNode`）を提供します。

- 時間はサンプル数で統一（浮動小数点誤差を避ける）
- **Transport**（再生位置）を持ち、最初からだけでなく任意の位置からの再生（シーク）に対応
- リアルタイム再生とオフラインレンダリングは同じグラフ・同じ処理経路を使う（プレビューと書き出し結果の不一致を防ぐ）

詳細な設計は [`highAudio.md`](./highAudio.md) を参照してください。

## Project Structure

```text
HighAudioGen
├── Core         # 最下層の共通ライブラリ。何にも依存しない
├── SDK          # プラグイン契約(IPlugin/IAudioNode/パラメータ定義等)。Coreに依存
├── AudioEngine  # ノードグラフ・Transport本体。Core + SDKに依存
├── Analysis     # FFT/Spectrogram等の解析ロジック。Coreに依存
├── Codecs       # コーデックプラグイン(WAV/MP3等)。Core + SDKに依存
├── Desktop      # Avalonia UIクライアント。AudioEngine + Analysis + Codecsに依存
└── Tests        # xUnit
```

## Technology

| 領域 | 技術 |
|---|---|
| Language | C# (.NET 10) |
| UI | Avalonia UI |
| Audio | NAudio |
| Database | SQLite |
| Plugin | AssemblyLoadContext |
| Testing | xUnit |
| Serialization | System.Text.Json |

導入予定: SIMD / NativeAOT / Source Generator / Reflection / Roslyn / Hardware Acceleration

## Goals

- Audio IDE
- Audio SDK
- Audio Analysis Tool
- Audio Development Environment
- Professional Audio Inspection Tool

## Non Goals

- 一般向けDAWの代替
- 初心者向け音楽制作ソフト
- AIを中心とした音楽生成ツール（AIはAudio Engineを利用するプラグインの一つとして扱う）
