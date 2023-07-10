# 頑張った点

## アニメーション
   
VBの中でアニメーションを実装する便利な機能が見つけられませんでした。
そのため、アニメーションを実現するために、
アニメーション用の描画ループを立ち上げ、一定時間ごとの画面の遷移を実現しました。

しかし、メインループとは別のループを使うと、ゲームのFPSが下がった時にアニメーションだけが
浮いてしまうという問題が発生しました。
そのため、メインループの中でアニメーションを処理するために、
メインループ内でフレームが処理された回数を表す「フレーム時間」を導入し、
アニメーションの開始フレーム時間、終了フレーム時間を管理することで、
メインループの中でアニメーションを実現しました。

無敵状態のアニメーションと終了処理
https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Game.vb#L166

ゲームオーバーのアニメーション
https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Game.vb#L277


## Stageの抽象化
敵の種類, ステータス, 出現頻度 などステージによって異なる部分を
各ステージのクラス内で定義することで、ステージに固有の処理、共通関数などを
一箇所にまとめました。
このことにより、ステージの追加/変更が容易になりました。

ステージの抽象クラス
https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L5

各ステージクラス内で、固有の敵の出現パターンを定義している様子
https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L150
https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L471

メインループの中では、ステージごとの分岐をすることなくステージ固有の処理を実行しています。
https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Game.vb#L137
