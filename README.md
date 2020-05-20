#  MusicGame(仮称)

Unityによる音ゲーの制作


# コミットメッセージ

フォーマットは以下のようにお願いします．
> [コミット種類] 変更内容の要約

コミット種類は以下の通りです．
* `fix` : バグ修正
* `hotfix` : クリティカルなバグ修正
* `add` : 新規（ファイル）機能追加
* `update` : 機能修正（バグではない）
* `change` : 仕様変更
* `clean` : 整理（リファクタリング等）
* `disable` : 無効化（コメントアウト等）
* `remove` : 削除（ファイル）
* `upgrade` : バージョンアップ
* `revert` : 変更取り消し


# ルール(随時追記)

* Alt+Shift+F(Win), Option+Shift+F(Mac)で整形を行なってください．
* スペース設定は4, UTF-8での記述をお願いします．
* mp3等の巨大なファイルのpushを行わないでください．
* コマンドラインからのmasterブランチへマージを行わず，Web上からPull requestを行なってください．


# Gitの基本的なコマンド

### ブランチ一覧を表示 : `$ git branch`
> 今いるブランチには，＊が表示されます．

### ブランチの作成を行う : `$ git branch <branchName>`
> ブランチを作成する際，今いるブランチを切って作成されます．(今いるブランチから伸びる)

### ブランチを切り替える : `$ git checkout <branchName>`
> ブリンチを切り替える際には，必ずコミットを行なってください．

### ブランチを切り替える : `$ git checkout <branchName>`
> ブリンチを切り替える際には，必ずコミットを行なってください．

### ファイルを追跡対象にする : `$ git add -A`
> 引数 -Aは全てのファイルを対象にします．ファイル名を指定して行いたい場合，引数なしでファイル名を入力します．

### コミット : `$ git commit -m <commitMessage>`
> コミットメッセージは，上記のルールに従ってください．種類は感覚で構いませんよ．

### プッシュ : `$ git push`
> 変更内容をプッシュすることで，他ユーザにも自分のブランチが反映されます．(現在のブランチのみ)

### フェッチ : `$ git fetch`
> リモートにある変更内容を取得します．(現在のブランチのみ)

### マージする : `$ git merge <branchName>`
> masterブランチへのマージを禁止しています！masterブランチへはWeb上でpull requestを送ってからマージします．masterブランチでの変更を自分のブランチ内へ取り込む時に使用します．取り込み先のブランチに移動し，branchNameには取り込みたいブランチを入力して行います．

マージ 例
```
$ git checkout smpny7
$ git merge master
```


# VSCodeの拡張機能

* [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
* [C# FixFormat](https://marketplace.visualstudio.com/items?itemName=Leopotam.csharpfixformat)
* [Git Graph](https://marketplace.visualstudio.com/items?itemName=mhutchie.git-graph)
* [Material Icon Theme](https://marketplace.visualstudio.com/items?itemName=PKief.material-icon-theme)
* [Trailing Spaces](https://marketplace.visualstudio.com/items?itemName=shardulm94.trailing-spaces)
* [zenkaku](https://marketplace.visualstudio.com/items?itemName=mosapride.zenkaku)


# 製作者

* smpny7
* chishige1217200
* Natsu-dev