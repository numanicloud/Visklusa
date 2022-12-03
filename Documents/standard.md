# Visklusa JsonZip 仕様

Visklusa JsonZipは、Visklusaを実装する際にvisk-notationとしてjsonを採用し、visk-archiveとしてzipを採用した場合の実装例です。

Visklusa JsonZipの実体はzipファイルであり、以下のファイルがアーカイブされています:

- layout.json
- assets.json
- 無制限な数の、画像などのアセット群

## layout.json

layout.jsonは例えば以下のような構造です。

```json
{
    "Capabilities": [
        "CapabilityName0",
        "CapabilityName1"
    ],
    "Elements": [
        {
            "Id": 0,
            "CapabilityName0": {
                "X": 100,
                "Y": 100
            }
        },
        {
            "Id": 1,
            "CapabilityName0": {
                "X": 10,
                "Y": 10
            },
            "CapabilityName1": {
                "visible": false
            }
        }
    ]
}
```

### Capabilities

Capabilitiesは文字列の配列です。layout.json内に出現するCapability名を全て列挙する必要があり、かつ出現しないCapabilityは含んではなりません。

### Elements

これはElementの配列です。

### Element

レイアウト要素を表すObjectです。

ElementはIdを持ちます。これはlayout.json内で他のElementに対して一意である必要があります。

Elementは任意のCapabilityを0個以上、Capability名をkeyに使いながら持ちます。

### Capability

任意のObjectです。Visklusa JsonZip ではCapabilityの中身について何も保証しません。

# Visklusa 仕様(一般)

Visklsuaは、visk-notation形式で書かれた設定ファイルを持ち、visk-archive形式でアーカイブ化されたファイル集合を指します。

アーカイブされていない状態のVisklusaは、以下のような構造のフォルダです：

- ルートフォルダ
  - layoutファイル
  - assetsファイル
  - 無制限な数の、画像などのアセット群

## visk-notation形式

visk-notation形式に決まったフォーマットはありません。ただし、後述するlayoutファイルとassetsファイルの機能を提供できる必要があります。

例えば、wav音声ファイル形式を用いてlayoutファイルを記述するのは好ましくありません。

visk-notation形式として考えられるのは、json, xml, yaml, toml, csvなどです。

## visk-archive形式

visk-archive形式に決まったフォーマットはありません。ただし、後述するlayoutファイルとassetsファイルの情報を不足なく読み込める必要があります。

例えば、jpegなどの非可逆的な圧縮を前提とするアーカイブ形式は好ましくありません。

visk-archive形式として考えられるのは、zip, lzh, tar, z7, あるいは単なるフォルダなどです。

## layoutファイル

layoutファイルはvisk-notation形式で書かれたテキストファイル、あるいはバイナリファイルです。
layoutファイルは以下の情報を含んでいる必要があります。

### Capabilities

これは文字列の配列です。このlayoutファイルに出現するcapability名を全て列挙する必要があり、かつこのlayoutファイルに出現しないcapability名を含んではなりません。

### Elements

これはElementの配列です。

### Element

レイアウト要素を表すためのデータです。

- Idを持ちます。1つのlayoutファイルにおいて、IdはElementごとに一意の値を持ちます。
- 任意のCapabilityを複数持ちます。文字列を用いてElement内のCapabilityを検索できるようにする必要があります。

### Capability

任意のデータです。VisklusaはCapabilityの中身について何も保証しません。

## assetsファイル

assetsファイルはvisk-archive形式で書かれたテキストファイル、あるいはバイナリファイルです。
