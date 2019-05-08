﻿Пилот
=====

Возможности: 
- [ ] Выбор пути к локальному репозиторию 
- [x] Выявлять даты создания долгов по дате коммита, в котором они появляются 
- [x] Отображать в окне список долгов
- [ ] Сохранение списка в текстовый файл

Ограничения: 
* Идентификация долга осуществляется по полному совпадению текста долга
* Работа осуществляется только с долгами, существующими в текущей версии репозитория
* Настройки по умолчанию задаются в конфигурационном файле
* Информация о долге ограничена датой создания, файлом и текстом долга
* Работа только с одним тэгом
* Поиск ограничен C# файлами ('.cs')
