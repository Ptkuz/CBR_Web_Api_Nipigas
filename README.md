# CBR_Web_Api_Nipigas
Часть 2 - Взаимодействие с WebApi Центробанка. Техническое задание НИПИГАЗ 

В данном решение представлена реализация взаимодействия с Web API ЦентроБанка.

Используемые технологии: .NET 6, C# 10, Visual Studio 2022, WPF, Паттерн MVVM

1. Проект построен на WPF + MVVM;
2. Используется Dependency Injection для взаимодействия с сервисами;
3. Длительные операции по чтению и составлению документов выполняются в ассинхронном режиме при помощи библиотеки TPL (async await)
4. Создан ProgressBar для визуализации процесса получения данных
