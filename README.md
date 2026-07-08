# 💬 SuperChack — P2P Мессенджер

> Мессенджер без центрального сервера. Общение напрямую между устройствами через Radmin VPN.

![.NET](https://img.shields.io/badge/.NET-10.0-purple)
![Avalonia](https://img.shields.io/badge/Avalonia-12.0-blue)
![SQLite](https://img.shields.io/badge/SQLite-EF%20Core-green)
![License](https://img.shields.io/badge/license-MIT-orange)

---

## 🌟 О проекте

**SuperChack** — это децентрализованный P2P мессенджер, где сообщения передаются напрямую между устройствами без сервера посредника. Проект разработан на .NET 10 с двумя интерфейсами — консольным и графическим.

---

## ✨ Возможности

- 📡 **P2P соединение** — прямая передача сообщений через TCP
- 🖥️ **Два интерфейса** — консольный и графический (Avalonia UI)
- 💾 **История сообщений** — хранение в локальной SQLite базе данных
- ✅ **Валидация** — проверка IP адреса и имени пользователя
- 📝 **Логирование** — запись событий через Serilog
- 🌍 **Кроссплатформенность** — Windows, Linux, macOS

---

## 🛠️ Технологии

| Технология | Назначение |
|---|---|
| .NET 10 | Платформа |
| Avalonia UI | Графический интерфейс |
| Entity Framework Core | ORM для работы с базой данных |
| SQLite | Локальное хранилище сообщений |
| CommunityToolkit.Mvvm | MVVM паттерн |
| FluentValidation | Валидация форм |
| Serilog | Логирование |
| Radmin VPN | Виртуальная сеть для P2P соединения |

---

## 🏗️ Архитектура
SuperChack/
├── SuperChack.Core          # Ядро — модели, сеть, база данных
│   ├── Models/              # Message — модель сообщения
│   ├── Network/             # Listener, Sender — TCP соединение
│   └── Storage/             # AppDbContext, ChatRepository — SQLite
│
├── SuperChack.ConsoleApp    # Консольный интерфейс
│
└── SuperChack.Avalonia      # Графический интерфейс
├── ViewModels/          # MVVM логика
├── Views/               # UI экраны
└── Validators/          # Валидация форм

---

## 🚀 Быстрый старт

### Требования
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Radmin VPN](https://www.radmin-vpn.com/) — для подключения между устройствами

### Установка

```bash
git clone https://github.com/xxGonzalesxx/P2P-SuperChack.git
cd P2P-SuperChack
```

### Запуск графического интерфейса
```bash
dotnet run --project SuperChack.Avalonia
```

### Запуск консольного интерфейса
```bash
dotnet run --project SuperChack.ConsoleApp
```

---

## 📖 Как использовать

1. Установи **Radmin VPN** на оба устройства
2. Создай общую сеть в Radmin VPN и подключи друга
3. Запусти **SuperChack** на обоих устройствах
4. Введи своё имя и **Radmin VPN IP** друга
5. Начни общаться 🎉

---

## 📁 Структура данных

Сообщения хранятся локально в `superschack.db`:

| Поле | Тип | Описание |
|---|---|---|
| Id | INTEGER | Уникальный идентификатор |
| Sender | TEXT | Имя отправителя |
| Text | TEXT | Текст сообщения |
| SentAt | TEXT | Дата и время отправки |

---

## 👨‍💻 Автор

**xxGonzalesxx** — студенческий проект, представленный на конференции 2026

---

## 📄 Лицензия

Проект распространяется под лицензией [MIT](LICENSE).
