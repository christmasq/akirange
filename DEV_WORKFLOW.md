# AkiRange 開發流程（DEV_WORKFLOW）

本文件定義 AkiRange 的日常開發節奏：**規劃 → 實作 → Push/PR → 網頁版 Codex Review → 修正 → 合併**。  
目標是讓每個功能都能用同一套流程重複套用，降低協作成本與返工。

---

## 1. 一次性設定（只做一次）

### 1.1 Repo 與分支策略
- `main`：穩定可跑、可隨時發布（保持乾淨）
- feature branch：`feature/<topic>`（例：`feature/calendar-auth`）
- hotfix（可選）：`hotfix/<topic>`

### 1.2 Commit 規範（建議）
- `feat:` 新功能
- `fix:` 修 bug
- `chore:` 工具/雜務/CI
- `docs:` 文件
- `refactor:` 重構（無功能變更）
- `test:` 測試

範例：
- `feat: add weekly plan generation endpoint`
- `fix: prevent duplicate calendar commits`

### 1.3 CI 基本要求
- PR 必須通過 GitHub Actions（Flutter + .NET）
- **主幹禁止紅燈合併**（除非緊急）

---

## 2. 每個功能的標準循環（你會一直重複用）

### Step 1：收斂需求（5–15 分鐘）
產出 3 件事即可：
1) **目標**：這次要解決什麼  
2) **範圍**：這次做哪些、不做哪些  
3) **驗收條件**：可測、可判定完成  

> 建議寫在 Issue 或 PR 描述中（不要只留在腦中）。

---

### Step 2：網頁版 Codex 產施工清單（不改 code）
用網頁版 Codex 把需求變成「檔案清單 + 實作步驟 + 測試策略」。

**建議 Prompt（可複製）**
```text
AkiRange 專案（Flutter + .NET 10 monorepo），請針對以下功能產出實作清單（不要直接寫程式碼）：

【功能目標】
...

【範圍（做/不做）】
- 做：
- 不做：

【驗收條件】
...

【輸出格式】
1) 需要改動/新增的檔案清單
2) API endpoints 與 request/response（如需要）
3) 測試策略（單元/整合）
4) 風險與注意事項
5) 建議的 commit 切分（1~3 個）
```

---

### Step 3：VS Code Codex 在 repo 內落地實作
在 VS Code 中交給 Codex：
- 依施工清單逐項實作
- 補測試
- 更新最小文件（README / docs/API.md）

**本機最小驗證**
- Flutter：`flutter analyze`、`flutter test`
- .NET：`dotnet test`、`dotnet build`

---

### Step 4：Commit & Push
```bash
git checkout -b feature/<topic>
git add .
git commit -m "feat: <topic>"
git push -u origin feature/<topic>
```

---

### Step 5：開 PR（務必寫清楚）
PR 至少要包含：
- 目的 / 驗收條件
- 變更摘要（條列）
- 測試方式（本機怎麼跑）
- UI 有改：附截圖
- 風險 / 回滾（有風險就寫）

---

## 3. Push 後：網頁版 Codex Review 標準流程

> **重點**：網頁版 Codex 通常無法直接讀 private repo，請用「Review Bundle」餵它 PR 資訊。  
> Bundle 模板見 `docs/REVIEW_BUNDLE.md`。

### Step A：整理 Review Bundle
建議用 git 指令快速整理（擇一）
```bash
git diff main...HEAD > /tmp/pr.diff
git diff --name-status main...HEAD
```

### Step B：把 Bundle 丟給網頁版 Codex
使用 `docs/REVIEW_BUNDLE.md` 的 Prompt 格式，貼上：
- PR 目標/驗收
- 檔案列表
- 關鍵 diff
- CI 結果（成功或失敗 log）

### Step C：把 Review 轉成可執行修正清單
優先順序建議：
1) **高**：Bug/邊界條件/安全（token、secret、log）
2) **中**：測試缺口、架構不穩、命名一致性
3) **低**：風格、文件小補強

修完後：
```bash
git add .
git commit -m "fix: address codex review feedback"
git push
```

---

## 4. 合併前檢查（每個 PR 都做）
- CI 綠燈（Flutter + .NET）
- 驗收條件都過（你自己點一次/跑一次）
- 無敏感資訊（client secret、refresh token、Authorization header）
- log 不印敏感資訊
- docs 最小更新（API 有變更就更新 `docs/API.md`）

---

## 5. 合併後（可選但很推薦）
- 在 PR 或 Issue 紀錄：本次決策、踩坑、後續待辦
- 需要的話打 tag：`v0.1.0-mvp1`

---

## 6. 建議每週節奏
- 週一：決定本週 1–2 個功能（目標+驗收）
- 週二～四：實作→PR→Codex review→修正→合併
- 週五：小回顧（CI 常壞？測試不足？命名漂？）
