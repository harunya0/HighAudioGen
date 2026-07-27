## 現状まとめ

**✅ 完了したこと**

- **設計方針**：Vision / Philosophy / Design Policy（Engine First・Plugin First・Core is Independent・疎結合など）を確定し、`highAudio.md`に書き出し済み
- **Audio Graph Architecture**：ノードグラフ方式（`SourceNode`/`EffectNode`/`MixNode`/`SpatialNode`/`OutputNode`）、時間はサンプル数で統一、`Transport`でシーク対応、`ISeekAware`でプラグインの状態復元を疎結合に、TrackBusは特別扱いせずEffectNodeの一種として実装する方針まで決定済み
- **ソリューション構成**：`Core`/`SDK`/`AudioEngine`/`Analysis`/`Codecs`/`Desktop`/`Tests`を作成し、依存方向（Core→誰にも依存しない、SDK→Core、AudioEngine→Core+SDK...）も設定済み
- **SDKの土台**：`IAudioNode`/`IAudioPort`/`AudioProcessContext`/`ISeekAware`を`SDK.Graph`に配置（AudioEngineとの循環参照を回避）、`IPlugin`は`IAudioNode`継承＋`Parameters`付きに修正、パラメータ定義・バリデーション系（`PluginParameterDefinition`系、`ParameterValidationResult`系、`IPluginParameterValidator`）も移植済み
- **Coreの土台**：`WaveData`/`Peak`/`WavePeakCache`/`WavePeakCacheBuilder`/`WaveFileReader`を配置済み
- **ビルド成功**を確認済み
- `README.md`作成済み、セットアップスクリプトは削除済み

**⬜ まだ手を付けていないこと**

- ノードグラフの**実行エンジン本体**（グラフを辿って`Process`を呼び、`MixNode`で合成する処理）
- `SourceNode`/`EffectNode`/`MixNode`/`SpatialNode`/`OutputNode`の**具体実装**
- `Transport`（再生位置管理・シーク処理）の実装
- `Codecs`：WAV/MP3の実デコード/エンコード
- `Analysis`：FFT/Spectrogramなど
- `Desktop`：Avalonia側のUI（タイムライン表示など）
- HRTF/Doppler/Spatial Audioの実装
- プロジェクト保存用JSONスキーマ（後回し方針のまま未着手）
- Developer Mode / Internal Modeの段階的公開の仕組み
- `AudioClip`の要否（保留のまま、必要になったら判断）

**➡️ 次にやるべきこと（優先順）**

1. **AudioEngineの実行エンジン本体**をまず動かす（グラフを辿って処理する最小限のループ）
2. `SourceNode`と`OutputNode`だけの最小構成で、**「WAVを1つ読み込んで再生できる」**ところまでを最初のマイルストーンにする
3. そこから`EffectNode`/`MixNode`/`Transport`（シーク含む）を足していく
4. 一通り動いたら`Codecs`（MP3対応）や`Analysis`（FFT等）へ広げる
