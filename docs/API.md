# AkiRange API

## Endpoints

`GET /health`
- 回傳服務狀態

`GET /api/version`
- 回傳 API 版本字串

`POST /tasks`
- 新增任務
- body: title (required), durationMinutes (> 0), occurrencesPerWeek (optional), earliestStartLocalTime (optional), latestEndLocalTime (optional)

`GET /tasks`
- 列出任務
