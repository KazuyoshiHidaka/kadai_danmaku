ここでは、「プレイ動画」と「制作にあたり頑張った点」の2つをご紹介します。


# プレイ動画

[プレイ動画.webm](https://github.com/KazuyoshiHidaka/kadai_danmaku/assets/49894531/429eecd5-7a53-4bc8-a233-a680ea34f8a4)

#### おまけ

<details>
<summary>ステージ1の中玉に当たると</summary>

[プレイ動画.webm](https://github.com/KazuyoshiHidaka/kadai_danmaku/assets/49894531/4df02b55-547e-4ee7-9587-b75f9dce6b9f)

</details>

<details>
<summary>ステージ1の最後の特大玉に当たると</summary>

[プレイ動画.webm](https://github.com/KazuyoshiHidaka/kadai_danmaku/assets/49894531/bcd9e4ff-2403-4425-949a-a956583956e2)

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

<img src="https://github.com/KazuyoshiHidaka/kadai_danmaku/assets/49894531/be9ffc54-3cda-4d98-9c25-09068b429812"
   width="150">

https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Game.vb#L166-L183

<br> 

例2）ゲームオーバーのアニメーション. 
※別の理由からメインループから切り離していますが、アニメーションにこだわって作りました

<img src="https://github.com/KazuyoshiHidaka/kadai_danmaku/assets/49894531/3f4f19cf-2516-4031-b5e9-4ef6e126c4e1"
   width="150">
   
https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Game.vb#L277-L299


<br><br>

## Stageの抽象化
敵の種類, ステータス, 出現頻度 などステージによって異なる部分を
各ステージのクラス内で定義することで、ステージに固有の処理、共通関数などを
一箇所にまとめました。
このことにより、ステージの追加/変更が容易になりました。

<br>

例1）ステージの抽象クラス
https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L5-L26

<br>

例2）各ステージクラス内で、固有の敵の出現パターンを定義している様子
[https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L150](https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L150)
[https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L471](https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Stage.vb#L471)

<br>

例3）メインループの中では、ステージごとの分岐をすることなくステージ固有の処理を実行しています。
https://github.com/KazuyoshiHidaka/kadai_danmaku/blob/fb1672b1cf4083d46ee1a6f9518bfe8e7cddb6e0/Game.vb#L134-L137
