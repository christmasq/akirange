# AkiRange – Step 3 實作流程（給 Codex 讀檔執行）

本文件是 **AkiRange MVP（Google Calendar）** 的實作施工圖，目的在於讓 Codex（VS Code / Web）可以**直接依文件逐段實作**，而不是重新思考需求。

---

## 專案前提
- Monorepo
  - `apps/akirange_app`（Flutter）
  - `services/akirange_api`（.NET 10 Web API）
- MVP 優先，可跑通即可
- 不開源，不新增 License
- 內部時間一律 **UTC**，對外（Flutter / Google）再轉 Asia/Taipei
- 行事曆：**Google Calendar（freebusy）**

---

# Commit / PR 切分總覽

- **A. Backend MVP（不碰 Google）**
  - A1：資料層 + SQLite + Entities
  - A2：Tasks CRUD（Create + List）
  - A3：Scheduler + Plans Generate（使用 stub 空檔）

- **B. Google 串接（閉環）**
  - B1：Google OAuth + Token 管理
  - B2：Google Calendar FreeBusy → Free Slots
  - B3：Commit 寫回行事曆 + 去重

- **C. Flutter 串接**
  - C1：API Client + AppState
  - C2：Login / Tasks / Plan / Commit Screens

---

# A. Backend MVP（不碰 Google）

## A1. SQLite + Entities + DbContext

### 目標
- 建立最小資料模型
- API 可啟動、可存取資料

### 實作清單
- 新增 EF Core + SQLite
- `AppDbContext`
- Entities：
  - TaskEntity
  - PlanEntity
  - PlannedItemEntity
  - CommitMappingEntity（先預留）

### A 段設計補強（為後續 Google 串接降風險）
- PlanEntity 必須包含 windowStartUtc / windowEndUtc / generatedAtUtc（用於對齊 FreeBusy 時段計算與追溯）
- PlannedItemEntity 必須包含 startUtc / endUtc（UTC），以支援後續 commit 寫回 Google
- CommitMappingEntity 必須包含 planId + taskId，並建立唯一索引（PlanId, TaskId）避免重複 commit
- 時間區間採用半開區間 [start, end) 避免相鄰時段重疊

### 驗收
- `dotnet run` 成功
- 可新增 Task 至 SQLite
- migrations 可建立

### Codex 指令
> Codex：請直接修改檔案完成上述事項，並列出需要執行的 migration 指令。

---

## A2. TasksController（Create + List）

### API
- `POST /tasks`
- `GET /tasks`

### 驗收
- title 必填
- durationMinutes > 0
- 可成功列出 Tasks

### Codex 指令
> Codex：請新增 TasksController 與 DTO，並更新 docs/API.md。

---

## A3. Scheduler + Plans Generate（Stub 空檔）

### 說明
- 先不用 Google
- 空檔固定為每天 **19:00–23:00（Asia/Taipei）**
- 系統內部仍以 UTC 計算

### Stub 空檔的時區轉換規則（Asia/Taipei → UTC）
- Stub 空檔以「Asia/Taipei 當地日期」為基準定義：每天 19:00–23:00（local）
- 實作時：先產生 localStart/localEnd（帶入 Asia/Taipei 時區），再轉成 UTC 存入 PlannedItem 的 startUtc/endUtc
- 區間採用半開 [start, end)，避免邊界重疊

### API
- `POST /plans/generate`

### 排程規則
- Greedy
- 任務不可切割
- 放不下 → unscheduled + reason

### 驗收
- 回傳 planId
- scheduled / unscheduled 正確
- 至少 2 個 unit tests

### Codex 指令
> Codex：請實作 SchedulerService 與 PlansController，並補 unit tests。

---

# B. Google 串接（閉環）

## B1. Google OAuth + Token 管理

### API
- `POST /auth/google/login`
- `GET /auth/google/callback`

### 規則
- Token 存 SQLite
- access_token 過期 → refresh
- refresh 失敗 → 401 + 重新登入

### 驗收
- 可成功登入 Google
- DB 中有 token
- refresh 流程可跑

### Codex 指令
> Codex：請新增 AuthController、TokenService，並更新 README 與 docs/API.md。

---

## B2. Google Calendar FreeBusy → Free Slots

### 說明
- 使用 FreeBusy API
- Input / Output 一律 UTC
- 回傳 free slots

### 驗收
- busy → free 計算正確
- 有 unit tests

### Codex 指令
> Codex：請實作 GoogleCalendarService 與 busy→free 轉換邏輯。

---

## B3. Commit（寫回行事曆 + 去重）

### API
- `POST /plans/{planId}/commit`

### 去重策略
- DB：`planId + taskId`
- 已存在 → skip
- 新增成功 → 存 googleEventId

### 驗收
- 第一次 commit：committed > 0
- 第二次 commit：skipped > 0
- 不會重複新增事件

### Codex 指令
> Codex：請新增 CommitController 並補 integration-style test（可 mock calendar）。

---

# C. Flutter 串接

## C1. API Client + AppState

### 目標
- 封裝後端 API
- 提供 loading / error state

### Codex 指令
> Codex：請建立 api_client.dart 與 app_state.dart。

---

## C2. UI Screens（最小）

### Screens
- LoginScreen（Google OAuth）
- TasksScreen（新增 / 列表）
- PlanScreen（Generate + 顯示）
- CommitScreen（寫回）

### 驗收
- 能走完整流程：Login → Tasks → Generate → Commit
- 有基本錯誤提示

### Codex 指令
> Codex：請完成最小可用 UI（placeholder 即可）。

---

# Review 建議流程（給網頁版 Codex）

PR 完成後，請準備：
- PR 目的與驗收
- 檔案列表（git diff --name-status）
- 關鍵 diff
- CI 結果

並使用 `docs/REVIEW_BUNDLE.md` 的 Prompt 進行 review。

---

# 完成定義（MVP Done）
- 使用者可登入 Google
- 新增任務
- 生成排程
- 一鍵寫入 Google Calendar
- 不會重複新增事件
