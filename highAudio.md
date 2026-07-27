# HighAudioGen - Technology & Design

## Vision

HighAudioGen は DAW を作るプロジェクトではありません。

HighAudioGen は **Audio IDE** を目指します。

音を編集するだけではなく、

- 音を解析する
- 音を設計する
- 音を開発する
- 音を可視化する
- 音を拡張する

ための統合開発環境を目標としています。

---

# Philosophy

HighAudioGen は初心者向けソフトではありません。

一般的な DAW が隠してしまう情報も、
必要ならすべて閲覧・編集できることを目標にしています。

基本はシンプル。

必要ならどこまでも深く。

Visual Studio のように、

- 通常モード
- Developer Mode
- Internal Mode

を段階的に開放できる設計を採用します。

---

# Design Policy

## Engine First

UI よりも Audio Engine を優先します。

Desktop は Engine のクライアントであり、
Audio Engine がこのプロジェクトの本体です。

---

## UI と Engine を完全分離

UI は描画のみを担当します。

Audio の編集・解析・計算はすべて Engine が行います。

将来的に

- Desktop
- CLI
- Web
- Plugin

など複数のフロントエンドから利用できる設計を目指します。

アーキテクチャイメージとして、HighAudioGen の Audio Engine の上に
YMM4 のような上位のUI/エンジンが乗ることも想定しています。
そのため Engine 側は、上位レイヤーが細かい調整を行えるだけの
粒度の細かい制御を提供することを重視します。

---

## Plugin First

ビルトイン機能も外部プラグインも
同じインターフェースを実装します。

特別扱いされる機能は存在しません。

Track/Bus のような一見特別扱いしたくなる機能も、
「複数入力を受け付ける EffectNode の一種」として実装し、
専用の特別な型を作らない方針とします。

---

## Core is Independent

Core は UI に依存しません。

Audio Engine は Avalonia を参照しません。

プラグインの内部実装についても Core/Engine は関知せず、
Plugin・UI・Core は疎結合を保ちます。
（例：シーク時の内部状態復元は、Engine 側が個々のプラグイン実装を
知ることなく、プラグインが任意実装できる拡張インターフェース
（`ISeekAware` 等）経由で行う）

---

## Safety

通常モードでは安全性を優先します。

Developer Mode では高度な設定を公開します。

Internal Mode では Audio Engine 内部まで編集可能にします。

---

# Project Structure

```text
HighAudioGen

├── Core
├── AudioEngine
├── Analysis
├── Codecs
├── SDK
├── Desktop
└── Tests
```

Core は最も低レベルの共通ライブラリです。

AudioEngine は音響処理の中心となります。

Desktop は Engine を利用する GUI クライアントです。

---

# Technology

## Language

- C# (.NET 10)

## UI

- Avalonia UI

## Audio

- NAudio

## Database

- SQLite

## Plugin

- AssemblyLoadContext

## Testing

- xUnit

## Serialization

- System.Text.Json

---

# Future Technology

今後導入予定

- SIMD
- NativeAOT
- Source Generator
- Reflection
- Roslyn
- Hardware Acceleration

---

# Audio Engine

AudioEngine は以下の機能を担当します。

- AudioBuffer
- WaveData
- PCM
- DSP
- FFT
- Spectrogram
- Metadata
- Spatial Audio
- HRTF
- Doppler
- Automation
- Plugin Processing

**Timeline は AudioEngine の機能範囲には含みません。**
Timeline はあくまで UI 側の「操作・表示」の責務であり、
Engine は「Timeline」という概念を持ちません。

ただし、音をタイムライン上で動かし・音と音を重ねる、という
編集操作を成立させるために、Engine は下記の
「Audio Graph Architecture」で示す下位プリミティブを提供します。

---

# Audio Graph Architecture

Timeline を UI 側の責務とする代わりに、Engine は以下のアーキテクチャで
「音の配置」と「重ね合わせ」を支えます。

## ノードグラフ方式

Engine はノードグラフ（JUCE の AudioProcessorGraph / Web Audio API 的な発想）
で音の配置・処理・重ね合わせを表現します。

- `SourceNode` — `WaveData` / `AudioClip` を音源として持ち、
  グラフ全体における絶対開始位置（サンプル数）を持つ
- `EffectNode` — DSP処理・Plugin Processingを1つ挟む
- `MixNode` — 複数入力を加算合成する
- `SpatialNode` — HRTF / Doppler / Spatial Audio を担当
- `OutputNode` — バイノーラル/ステレオの最終出力

Track/Bus（ソロ・ミュート・バスエフェクトなど）は専用ノード型を作らず、
「複数入力を受け付ける `EffectNode` の一種」として実装します。
Track機能自体もビルトインプラグインとして提供可能な設計とします。

## 時間の単位

Engine内部の時間表現は **サンプル数** で統一します。
浮動小数点の丸め誤差を避け、正確な重ね合わせ・シークを実現するためです。

## Transport（再生位置）とシーク

Engineは再生位置（プレイヘッド）を管理する **Transport** を持ちます。

- 最初からだけでなく、任意の位置から再生を開始できる（DAWと同様の使用感）
- シークした際、Engineはそのプレイヘッド時点で鳴っているはずの
  全 `SourceNode` を計算し、各ノードの内部再生位置を進めた状態から処理を始める

### ステートを持つエフェクトのシーク対応

リバーブの残響など内部状態を持つエフェクトは、シーク直後は理論上
完全に正確な音にはなりません。この課題への対応は、Plugin First /
疎結合の方針に基づき、以下の形で解決します。

- `Plugin.Abstractions` に `ISeekAware` のような **追加インターフェース** を用意する
  （`IPlugin` 本体には含めない、任意実装）
- Engineは各ノードの処理が `ISeekAware` を実装しているかどうかだけを見て
  呼び出す。実装内容には関知しない
- 未実装の場合は素朴なシーク（内部状態を作り直さない）にフォールバックする

これによりEngineは個々のプラグインの実装を一切知る必要がなく、
Core is Independent の方針を保ったまま拡張できます。

## リアルタイム再生とオフラインレンダリングの統一

リアルタイム再生（Transportでの再生）とオフラインレンダリング
（バイノーラル/ステレオでのファイル書き出し）は、
**同じノードグラフ・同じ処理経路** を使います。

- 「聴いた音」と「書き出し結果」が必ず一致するという信頼性を優先
- プラグイン実装もリアルタイム/オフラインで1系統のみで済む
- プラグイン側には「非等速（早い/遅い）で呼ばれる可能性がある」ことを
  契約として明示する（wall-clock時間に依存した処理をしないこと、等）

---

# Goals

HighAudioGen が目指すもの

- Audio IDE
- Audio SDK
- Audio Analysis Tool
- Audio Development Environment
- Professional Audio Inspection Tool

---

# Non Goals

HighAudioGen は以下を目的としません。

- 一般向け DAW の代替
- 初心者向け音楽制作ソフト
- AI を中心とした音楽生成ツール

AI は Audio Engine を利用するプラグインの一つとして扱います。

Audio Engine 自体が主役です。

---

# Motto

Understand Audio.

Inspect Audio.

Develop Audio.
