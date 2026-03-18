# Smart EMR & Dev Assistant AI Agent

本專案為結合醫療領域知識與 AI 技術的實作展演。旨在將所學之專業知識轉化為具備擴充性的 API 系統，完整展示從架構設計、資料庫建置到大語言模型串接的軟體工程歷程。

---  

## 簡介 :  
這是一個專為醫療場域與軟體工程團隊打造的智慧病歷與輔助 AI Agent 後端系統。
本系統結合了醫療資訊領域知識（EMR）與大語言模型（LLM），目的是希望可以降低醫護人員查詢資料的負擔，並透過 AI 自動化輔助軟體工程師生成測試案例。

## 專案動機與學習目標
* 實作病患與就診紀錄的關聯式資料庫，提供穩定的 CRUD API 介面。
* 導入 Git 版本控制、Entity Framework Core 開發模式，並確保專案結構符合業界標準。
* 串接 LLM，實現自然語言查詢與病歷摘要，大幅節省人工調閱病歷的時間。

<br>

## 技術堆疊
* Backend: C# ASP.NET Core Web API (.NET 8)
* Database: SQLite, Entity Framework Core
* Version Control: Git, GitHub
* AI Integration: Google Gemini 2.5 Flash API (透過 User Secrets 確保金鑰安全)

<br>

## 開發過程記錄 & 階段成果

### [ 階段一 ] 基礎架構與資料庫設計 (已完成)
為了快速建構輕量且可攜的測試原型，本專案選用 **SQLite** 作為資料庫，並以 Entity Framework Core 進行資料庫遷移。
* 建立 Patient 與 MedicalRecord 的一對多關聯模型。
* 克服遇到的 dotnet-ef 全域環境變數路徑問題，改採專案區域工具 (Local Tool) 確保依賴性隔離。

<br>

### [ 階段二 ] 核心 RESTful API 開發 (已完成)
遵循 REST 系統架構風格，實作 PatientsController 與 MedicalRecordsController。
* 提供新增病歷、查詢特定病患所有就診紀錄等核心端點。
* 透過 **Swagger UI** 進行本地端點測試，確認 JSON 資料格式回傳正確。

<br>

### [ 階段三 ] AI Agent 核心邏輯與醫療摘要生成 (已完成)
運用 Dependency Injection 建立專屬的 AI 服務層，維持 Controller 邏輯的整潔與關注點分離。
* 使用 **Google Gemini 2.5 Flash API** 處理醫療脈絡以及生成文本。
* 設計專業醫療助理角色，要求模型根據歷史就診紀錄，輸出包含主要**症狀總結**、**歷次診斷變化趨勢**與**後續照護建議**的結構化繁體中文報告。
* 採用 .NET 內建的 **User Secrets** 機密管理員，確保 API Key 不會外流至版控系統。

<br>

---

### [Future Work] 前端開發整合
* 之後預計開發 Web 前端介面，提供醫護人員視覺化的病患列表與就診歷史，並能直接於介面上觸發 AI 摘要生成功能，完善整體使用者體驗。