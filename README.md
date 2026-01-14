# AkiRange
Automatically arrange tasks into your available calendar time.

## 資料夾結構
```
apps/akirange_app     # Flutter app
services/akirange_api # .NET Web API
docs/                 # 產品與架構文件
.github/workflows/    # CI
```

## 本機開發

### Flutter
```bash
cd apps/akirange_app
flutter pub get
flutter run
```

### API
```bash
cd services/akirange_api
dotnet run
```

API 預設會啟動 Swagger UI：`https://localhost:5001/swagger`
