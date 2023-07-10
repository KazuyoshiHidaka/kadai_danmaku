ここでは、「プレイ動画」と「制作にあたり頑張った点」の2つをご紹介します。


# プレイ動画

[kadai_danmakuプレイ動画.webm](https://github.com/KazuyoshiHidaka/kadai_danmaku/assets/49894531/f09b51ce-b6b8-4f2a-8627-388c6cccc686)

#### おまけ

<details>
<summary>ステージ1の中玉に当たると</summary>

https://github.com/KazuyoshiHidaka/kadai_danmaku/assets/49894531/75bceca3-20ae-42a7-a164-6c37345310a2

</details>

<details>
<summary>ステージ1の最後の特大玉に当たると</summary>

https://github.com/KazuyoshiHidaka/kadai_danmaku/assets/49894531/c1ed6e68-2260-4b47-8250-53c053f08020

</details>


# 頑張った点

## アニメーション
   
VBの中でアニメーションを実装するための便利な機能を見つけることができませんでした。
そのため、アニメーションを実現するために、
アニメーション用の描画ループを立ち上げ、一定時間ごとの画面の遷移を実現しました。

しかし、メインループとは別のループを使うと、ゲームのFPSが下がった時にアニメーションだけが
浮いてしまうという問題が発生しました。
そのため、メインループの中でアニメーションを処理するために、
メインループ内でフレームが処理された回数を表す「フレーム時間」を導入し、
アニメーションの開始フレーム時間、終了フレーム時間を管理することで、
メインループの中でアニメーションを実現しました。

<br>

例1）無敵状態のアニメーションと終了処理 

<img src="https://github.com/KazuyoshiHidaka/kadai_danmaku/assets/49894531/6cac3e31-2282-431d-b0a6-7a99e5ee85ea"
   width="150">

https://github.com/KazuyoshiHidaka/yokero/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Game.vb#L166-L183

<br> 

例2）ゲームオーバーのアニメーション. 
※別の理由からメインループから切り離していますが、アニメーションにこだわって作りました

<img src="https://github.com/KazuyoshiHidaka/kadai_danmaku/assets/49894531/ac894c9d-2a61-4354-a4f5-a6cd1e71c8ff"
   width="150">
   
https://github.com/KazuyoshiHidaka/yokero/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Game.vb#L277-L299


<br><br>

## Stageの抽象化
敵の種類, ステータス, 出現頻度 などステージによって異なる部分を
各ステージのクラス内で定義することで、ステージに固有の処理、共通関数などを
一箇所にまとめました。
このことにより、ステージの追加/変更が容易になりました。

<br>

例1）ステージの抽象クラス
https://github.com/KazuyoshiHidaka/yokero/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L5-L26

<br>

例2）各ステージクラス内で、固有の敵の出現パターンを定義している様子
[https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L150](https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L150)
[https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L471](https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L471)

<br>

例3）メインループの中では、ステージごとの分岐をすることなくステージ固有の処理を実行しています。
https://github.com/KazuyoshiHidaka/yokero/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Game.vb#L134-L137
