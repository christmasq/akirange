# AkiRange PR Review Bundle（給網頁版 Codex）

此文件提供一個「可複製貼上」的 Review Bundle 模板，用來把 PR 資訊餵給網頁版 Codex 做 code review。  
目標：讓 AI reviewer 能在缺乏 repo 存取權限的情況下，仍能做有效 review。

---

## 1) 你需要準備的資訊（最少 4 樣）
1. **PR 目的 / 驗收條件**（一句話+條列即可）
2. **變更檔案列表**（`git diff --name-status`）
3. **關鍵 diff**（只貼重要檔案即可；太長可分段）
4. **CI 結果**（成功摘要或失敗 log）

---

## 2) 建議的 git 指令（擇一使用）
### 2.1 產出檔案列表
```bash
git diff --name-status main...HEAD
```

### 2.2 產出完整 diff（可選）
```bash
git diff main...HEAD > /tmp/pr.diff
```

### 2.3 只看特定檔案 diff（建議更精準）
```bash
git diff main...HEAD -- services/akirange_api/Program.cs
git diff main...HEAD -- apps/akirange_app/lib/
```

---

## 3) 貼給網頁版 Codex 的 Prompt（直接複製）
```text
請幫我 review 這個 PR（AkiRange，Flutter + .NET 10 monorepo）。

【PR 目的 / 驗收條件】
- 目的：
- 驗收條件：

【變更檔案列表（name-status）】
（貼上 git diff --name-status main...HEAD 的輸出）

【關鍵 diff】
（貼主要檔案 diff；可分段貼，多貼幾次也可）

【CI 結果】
（貼 GitHub Actions summary 或失敗 log）

請用「高→中→低」優先級輸出 review：
1) Bug/邏輯錯誤、邊界條件
2) 安全性/敏感資訊（token、secret、logging）
3) 架構與可維護性（命名、抽 service、重複）
4) 測試缺口（哪些 case 一定要補）
5) Flutter UX（狀態、導頁、錯誤處理）
最後請給「建議修正清單（可直接變成下一個 commit）」。
```

---

## 4) 建議：怎麼切 diff 才不會太長
- 先貼：
  - endpoints / service / state management 的核心檔案
- 再貼：
  - workflow、config、依賴更新
- 最後貼：
  - tests 相關檔案

---

## 5) 收到 review 後的建議處理方式
- 高優先：先修 bug / 安全 / 明顯錯誤
- 中優先：補測試、抽共用、整理命名
- 低優先：格式、文件小修

修完 push 後，可以回到網頁版 Codex：
- 把「修正後的 diff」再貼一次做第二輪 review（通常 1 次就夠）。
