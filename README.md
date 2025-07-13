# Chaos Stage

用C#語言製作的小遊戲，特點為操作簡單直覺，但每關都有不同特殊事件，充滿驚喜。  
以瑪麗貓為起點做發想，像這樣類型的遊戲對製作者而言邏輯單純、能夠發揮創意，對玩家而言操作好上手、有驚喜感，所以就以此為主題。  

<img width="735" height="auto" src="https://github.com/user-attachments/assets/8a5d49ed-d46d-4dc6-9e16-971a9ce1b024" />  

> 目錄  
> [1. 使用方法](#1-使用方法)  
> [2. 前情提要](#2-前情提要)  
> [3. 遊戲流程與操作說明](#3-遊戲流程與操作說明)  
> [4. 功能介紹](#4-功能介紹)  
> [5. 關卡設計](#5-關卡設計)  
> [6. 使用素材](#6-使用素材)  
> [7. 畫面展示](#7-畫面展示)  

---

## 1. 使用方法

### 方法一：Google 雲端硬碟下載

1. 直接至雲端硬碟下載完整專案檔 : [link](https://drive.google.com/drive/folders/1GxGA044OjvL0n76FjneSvnJnv4I0C4xA?usp=sharing)
2. 解壓縮檔案
3. 至Chaos_Stage\bin\Debug資料夾，執行Chaos_Stage.exe
4. 開始遊戲

### 方法二：Github 下載

1. 在右上角點擊Code > Download ZIP下載檔案
2. 解壓縮專案檔
3. 下載額外的BGM檔 : [link](https://drive.google.com/file/d/100j4zrnCrr9OdXt-wYvEuDBkqee88ENw/view?usp=sharing)  
4. 將BGM檔放到Chaos_Stage\Resources資料夾中
5. 至Chaos_Stage\bin\Debug資料夾，執行Chaos_Stage.exe
6. 開始遊戲

---

## 2. 前情提要

<img width="auto" height="160" align="left" src="https://github.com/user-attachments/assets/15c788db-b17e-4079-bb26-f5dbb35255f7"/>

這是一隻狸貓，他叫帕拉  
今天天氣很好，帕拉想要去野餐  
經過河邊的時候，本來要帶去吃的棉花糖不見了  
但是沒有關係，帕拉還有一根冰棒  
冰棒覺得很熱，又感受到要被吃掉的生命危險，非常害怕
所以他飛到樹上去躲起來了  
於是帕拉就踏上了尋找冰棒的旅程  

---

## 3. 遊戲流程與操作說明

<img width="735" height="auto" src="https://github.com/user-attachments/assets/80fe544d-bccf-4a9d-8633-9d5f9614c158" />

- 每一關地形相同、起始位置相同，但事件不同
- 操作：左右移動、上跳躍、空白鍵射擊
- 碰到怪物或障礙會死掉、拿到寶箱就算過關（但可能有例外）

---

## 4. 功能介紹  

|功能|說明|
|:---:|---|
|**選擇關卡**|在選擇關卡畫面點擊不同按鈕可以從不同關卡開始|
|**角色移動**|按左右移動、上跳躍（無法跑出地圖外）|
|            |敵人會在一定範圍內巡邏|
|            |角色和敵人圖片都真的會動|
|**角色發動攻擊**|按空白鍵發射子彈攻擊敵人|
|**碰撞判定**|角色碰到障礙物、敵人會死亡|
|            |子彈碰到敵人，敵人會死亡|
|            |角色碰到冰棒就會過關|
|**暫停遊戲**|按暫停按鈕暫停遊戲|
|**跳過關卡**|真的受不了的話可以按跳過按鈕跳過這一關|
|**重力模擬**|角色掉落速度會隨著掉落時間變快，模擬重力|
|            |走出地板外會自動掉落|
|**畫面縮放**|不管畫面怎麼縮放都還是在正中間|
|**背景音樂**|遊戲中會播放背景音樂，比較不無聊一點|
|**通關畫面**|成功過關和闖關失敗會有不同的結算畫面、音效|
|            |主要按鈕按下時也會有音效|
|**操作顯示**|右下角即時顯示使用者按鍵，讓旁觀者比較清楚某些關卡事件發生了什麼事|

---

## 5. 關卡設計 

|關卡|事件|
|---|---|
|**第一關**|無事件，正常關卡|
|**第二關**|角色左右控制相反|
|**第三關**|怪物會高速反彈子彈，角色碰到怪物反彈的子彈會死掉|
|**第四關**|角色在跳過障礙的時候，障礙會往前移動|
|**第五關**|畫面會反覆變暗、變亮（5秒/次），但只有變暗的時候可以移動|
|**第六關**|怪物變成3隻，其中兩隻無敵|
|**第七關**|共出現3個寶物，只有其中一個是真的，另外兩個是陷阱（碰到會重新開始）|
|**第八關**|平台隱形，吃到寶物之後，會重新開始並出現下一個寶物，每次平台都會換位置|
|**第九關**|所有平台消失，沒有隱形平台，要用滑鼠拖動角色上去角色跳躍鍵失效，要用滑鼠點擊角色來跳躍|
|**第十關**|角色跳躍鍵失效，要用滑鼠點擊角色來跳躍|

---

## 6. 使用素材  

### 背景圖片、音樂

[Moonwind](https://moonwind.pw/index.html)  

<img width="735" height="auto" src="https://github.com/user-attachments/assets/33e24d49-6518-4d7f-875a-ea8336eddb0f" />

### UI圖示

[Kenny](https://kenney.nl/assets/game-icons)  

<img width="735" height="auto" src="https://github.com/user-attachments/assets/e6a823bf-2e2d-45f3-a7dd-1d407665d540" />

### 角色、障礙物、敵人、子彈圖示

[dotown.maeda](https://dotown.maeda-design-room.net/)  

<img width="735" height="auto" src="https://github.com/user-attachments/assets/4d88a530-c4d8-4094-98ba-c763456557b7" />

### 按鈕音效

[効果音ラボ](https://soundeffect-lab.info/sound/button/)  

<img width="735" height="auto" src="https://github.com/user-attachments/assets/7f358acf-0c8a-436d-bbec-1e975b4012af" />

---

## 7. 畫面展示

### 開始畫面

<img width="735" height="auto" src="https://github.com/user-attachments/assets/80d2d682-e820-499a-af76-657fc68cf11f" />

### 遊戲說明畫面

<img width="735" height="auto" src="https://github.com/user-attachments/assets/f22e9304-9cc7-4fa1-884a-48d9eb8be6bb" />

### 關卡選擇畫面

<img width="735" height="auto" src="https://github.com/user-attachments/assets/64f49c29-b960-4328-a2b5-f9433875277b" />

### 遊戲中

<img width="735" height="auto" src="https://github.com/user-attachments/assets/9a939803-bd4e-4f78-82dc-45c7e6541b26" />

### 暫停畫面

<img width="735" height="auto" src="https://github.com/user-attachments/assets/46855b68-e6a3-451a-aa5f-1e055f7d6a15" />

### 過關畫面

<img width="735" height="auto" src="https://github.com/user-attachments/assets/fe27ffc5-436b-4e06-aa53-9f274cf1141f" />

### 闖關失敗

<img width="735" height="auto" src="https://github.com/user-attachments/assets/3446183f-ba0a-4534-8233-3b1c2440b2ff" />

### 成功通關

<img width="735" height="auto" src="https://github.com/user-attachments/assets/d14f4441-0b6e-4633-9304-4493731ebf71" />
