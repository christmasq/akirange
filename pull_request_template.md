<!--
AkiRange Pull Request Template
- 目的：讓 reviewer（含網頁版 Codex）能快速掌握背景、變更、測試、風險
- 建議：盡量填，至少填「目的/變更/測試」
-->

## 目的 / 驗收條件
- 目的：
- 驗收條件（可測）：

## 變更內容（摘要）
- 

## 影響範圍
- [ ] Flutter（apps/akirange_app）
- [ ] .NET API（services/akirange_api）
- [ ] CI / DevOps
- [ ] Docs

## 測試方式
### Flutter
- [ ] `flutter analyze`
- [ ] `flutter test`
- 其他（手動驗證/截圖）：

### .NET
- [ ] `dotnet test`
- [ ] `dotnet build`
- 其他（手動呼叫 endpoint）：

## 風險與回滾
- 風險：
- 回滾方式：

## 截圖 / 錄影（如有 UI 變更）
- 

## 相關連結
- Issue：
- 設計/規格文件：

---

## （可選）給網頁版 Codex Review 的 Bundle
> 若要讓網頁版 Codex review，請貼上以下資訊（可用 `docs/REVIEW_BUNDLE.md` 的格式）

- PR 目標/驗收：
- 變更檔案列表（name-status）：
- 關鍵 diff：
- CI 結果/失敗 log：
