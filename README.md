# Smart EMR & Dev Assistant AI Agent

本題目內容旨在將課堂中學習過或自己有興趣的專業知識，以實際的專案開發來驗證學習成效所創作之作品。
---  
簡介:  
這是一個專為醫療場域與軟體工程團隊打造的「智慧病歷與輔助 AI Agent」後端系統。
本系統結合了醫療資訊領域知識（EMR）與大語言模型（LLM），目的是希望可以降低醫護人員查詢資料的負擔，並透過 AI 自動化輔助軟體工程師生成測試案例。

## 專案動機與學習目標
1. 實作病患與就診紀錄的關聯式資料庫，提供穩定的 CRUD API 介面。
2. 導入 Git 版本控制、Entity Framework Core (Code-First) 開發模式，並確保專案結構符合業界標準。
3. 串接 LLM，實現自然語言查詢病歷摘要與自動生成 API 測試案例。

## 技術堆疊
* **Backend**: C# ASP.NET Core Web API (.NET 8)
* **Database**: SQLite, Entity Framework Core
* **Version Control**: Git, GitHub
* **AI Integration**: 即將導入 OpenAI API

## 開發歷程與階段成果

### [ 階段一 ] 基礎架構與資料庫設計 ( 目前已完成 )
為了在有限時間內快速建構輕量且可攜的測試原型，本專案選用 **SQLite** 作為資料庫，並以 **Entity Framework Core (EF Core)** 進行資料庫遷移 (Migrations)。
* 建立 `Patient` (病患) 與 `MedicalRecord` (就診紀錄) 的*一對多關聯模型*。
* 成功克服 `dotnet-ef` 全域環境變數路徑問題，改採專案區域工具 (Local Tool) 確保依賴性隔離。

### [ 階段二 ] 核心 RESTful API 開發 ( 目前已完成 )
遵循 REST 系統架構風格，實作 `PatientsController` 與 `MedicalRecordsController`。
* 提供新增病歷、查詢特定病患所有就診紀錄等核心端點。
* 透過 Swagger UI 進行本地端點測試，確認 JSON 資料格式回傳正確。

### [ 階段三 ] AI Agent 核心邏輯 (進行中)
* 規劃實作：自然語言意圖辨識 ➔ 內部 API 呼叫 ➔ 醫療摘要生成 / 測試案例生成。

---
*本專案為 2026 實習申請之實作展示。*