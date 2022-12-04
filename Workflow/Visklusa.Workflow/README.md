# Visklusa.Workflow

よく実行するワークフローをGUIで簡単に実行したい

以下のようなcsxで実行したい

```csharp

var version = await $"nbgv get-version {projectPath}";

await $"dotnet pack {projectPath} -o Pack/";

```

- スクリプト内ではZxが有効
- 設定ファイルでボタン名とスクリプトファイルを紐づけたい
- 複数のコマンドをまとめて実行するコマンドを使えるようにしたい
  - csx内で `RunCommand(command-id)` みたいなのを呼べるようにする？
- iniファイルの値をスクリプト内から呼び出したい
- 🍰ランチャー機能を持たせてRiderとかForkとかを起動させたい
- 🍰ログをコマンドごとにフィルタリングしたい（コマンドが並列実行されるので）

参考
https://neue.cc/2021/08/23_602.html

でもこれで片付くかも
https://github.com/Cysharp/ConsoleAppFramework/