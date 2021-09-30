# CardRotager
Программа для работы с изображениями - центрование отсканированных карт

Скриншоты: Главное окно для обработки изображений (файл-пример 'test.jpg' находится в подпапке sample)
![mainForm](https://user-images.githubusercontent.com/17400519/135347407-edb79c62-1d04-43e7-a40d-39387d920bf8.jpg)

Закладка с настройками и отображения определенных карт и огибающих линий (вертикальной и горизонтальной для каждой карты)
![settingTab1](https://user-images.githubusercontent.com/17400519/135534384-bbc0d053-4909-4b4d-adea-bed79dfd9bd5.jpg)


﻿License: use the code with the notification of the author. Cannot be used in commercial applications. You can contact the author via e-mail. Author's email: sokrata@yandex.ru
  
Лицензия: используйте код с уведомлением автора. Нельзя использовать в коммерческих приложениях. Связаться с автором можно посредством электронной почты. Электронная почта автора: sokrata@yandex.ru

История изменений:
30/09/2021: 1. Добавлена возможность работать не только с 4 строками (рядов) карт, но и другое количество (например 5). Число колонок карт по прежнему строго две.

2. Добавлена настройка устанавливать минимальный размер карт для расчета сетки размещения карт в их центрах. Величина в пикселях (не см). Значения по умолчанию: MinCardWidth = 4160 px + 150 px (8.8 см + 150 px). MinCardHeight = 2965 px + 150 px (6.3 см + 150 px). 150 - это подстраховка. Раньше настройки были неизменяемые (зашиты в код).

3.  Добавлена настройка отображать каждую найденную карту по вертикали (без зеркалирования по рядам карт)

4.  Добавлена настройка не центрировать карты в результирущей картинке

5. На время обработки изображений скрываются изображения черновика и результата, чтобы избежать ошибок вызывающих аварийного закрытия программы.

6. В настройках установлены новые значения по умолчанию: вращать картинку - да, отображать найденные горизонтальные (бордовым цветом) и вертикальные линии (синим) карт, а также угла наклона карт (салатовым) на черновике - да. Показывать линейку на черновике - да.


29.09.2021: Добавлен архив с Release-версией

29.09.2021: 1. Создание формы для понижения разрешения. Для вызова вместо главной формы можно указать в командной строке ключ /changeImage или вызвать через главное меню Формы -> Изменить разрешение. 
2. Добавлена возможность понижать разрешения для целевой картинки. 
3. Возможность быстро сохранять картинку (по Alt+S или Alt+2) без показа диалога, если указана папка сохранения и был открыт файл. 
4. Возможность открыть картинку по Alt+1 (чтобы было удобно быстро открыть, а по Alt+2 сохранить результирующую картинку). 
5. Настройки формы Изменить разрешение сохраняются в файл settings2.xml, рядом с settings.xml основной формы в папке C:\Users\ИМЯ ПОЛЬЗОВАТЕЛЯ\AppData\Roaming\CardRotager\CardRotager\1.0.0.0

26.09.2021: По F4 не происходит перезагрузка картинки, а используется загруженная (на которой можно нарисовать белые квадраты, для зарисовывания мусор что мешает определить ровную линию), как и при нажатии кнопки Обработка.

26.09.2021: 1. Переработаны группировка настроек. 
2. Добавлен класс логера Logger вместо StringBuilder с возможностью локализации. 
3. Добавлен параметры вывода текста над изображением в параметрах DrawTextOnTargetImage и его шрифте DrawTextTargetFont

25.09.2021: Работа с PropertyGrid и настройками: Настройки вынесены в класс отображения PropertyObject и динамически подготавливают свойства. Добавлены новые методы сохранения и загрузки настроек. Подготовка к локализации свойств на другие языки

25.09.2021: Добавлены свойства по умолчанию для цветов. Рефакторинг кода.

25.09.2021: Переделано выравнивание 8 карт по сетке 4 строки и 2 колонки, исходя из размера карт ширина 4160 пикселей (что равно 8.8 см) + 300 пикселей и высота карт 2965 пикселей (что равно 6.3 см) + 300 пикселей. Расчет сделан при разрешении 1200 пикселей на дюйм. 300 пикселей это запасной отступ от края

25.09.2021: Переделано выравнивание 8 карт по сетке 4 строки и 2 колонки, исходя из размера карт ширина 4160 пикселей (что равно 8.8 см) + 300 пикселей и высота карт 2965 пикселей (что равно 6.3 см) + 300 пикселей. Расчет сделан при разрешении 1200 пикселей на дюйм. 300 пикселей это запасной отступ от края

24.09.2021: При ошибке прогресс бар и фон сообщения в панели состояния меняется на красный цвет

24.09.2021: Настройка SaveEachRectangleFileName переработана из SaveEachRectanglePath. Теперь можно использовать символы автоподстановки и указывать путь (автосоздаваемый, если его не существует). Можно выбрать расширение файла. Подсказка в описании свойства на закладке Черновик. Копия описания: Путь к папке и имя для сохранения найденных карт на картинке (пример: c:\\temp\\{fno}\\img{#}.bmp). Доступны автозамены: {#} на <номер карты>, {fn} на имя главного файла, {fno} на имя глав.файла без расширения и точки. Вместо bmp можно подставить расширения jpg, png, tif. Если не заполнено, не сохраняется

22.09.2021: 1. Добавлен пример в папке Sample для обработки. Сейчас поддерживается только карты на скане в 2 колонки и 4 строки. 
2. Добавлена возможность стирать часть загруженного изображения в Оригинале. После загрузки при 100% можно щелчками мыши затирать белым квадратом щёлкнутое место. 
3. Возможность отображать Точки реза (CutMark) на результирующем изображении и настраивать его отображение или его параметры 
4. Отображение прогресс Обработки картинки в строке состояния 
5. Вынос в настройки некоторых параметров в разделе Обработка в PropertyGrid на закладке Черновик

21.09.2021: 1. Настройки вынесены в отдельный класс Settings 
2. Настройки отображаются через PropertyGrid на закладке Черновик, но их локализация не работает 
3. Улучшено определение наклона линий
4. Возможно отображение точек и микролиний горизонтальной линии для указанной карты. Для этого введена настройка showDetailDotsOfImageNumber (если указан номер карты то для него покаывается синим - точки на основе которых строятся горизонтальные линии, малиновым - (промежуточные) микролинии из которых формируется итоговая горизонтальная линия 
5. Добавлена возможность горизонтально отражать карты (Horizontal Flip) внутри себя (чтобы текст был читаемым) и карты на самом листе. Настройка включается установкой flipHorizontalEachRect в true. 
6. Добавлена настройка установки flipHorizontalEachRect в true для текущего файла если он соответствует маске файла  в настройке flipHorizontalEachRectFileMask 
7. Добавлена возможность сохранять результат обработки в разные форматы и предлагать по умолчанию тот же тип файла что и исходный 
8. Добавлена настройка пользовательского пути для сохранения файлов customSavePath

20.09.2021: 1. Переделан механизм распознавания и поворота картинок. 
2. Добавлены настройки параметров (смотрите на закладке Черновик) 
3. Добавлены зумы в разные масштабы. Есть режим заполнения и режим масштабирования (зумы для этого режима) 
4. Добавлен узкий моноширный шрифт для отображения лога обработки 
5. В логе картинки выводятся по колонкам (1 это верхняя левая, 2 это под верхней левой и т.д.)

29.08.2021: = Открытие файла + Вставки из буфера исходного изображения

29.08.2021: = Рефакторинг ImageProcessor = Отрисовка линейки на черновик

29.08.2021: + настройка отображения линий (на закладке 'Черновик') + настройка применения вращения изображений карт

29.08.2021: + Применение наклона карт к результирующей центровка! (пока угол зачастую определяет не верно) + Отображение линии наклона на черновике. + Возможность переключаться на русский если текущий язык не русский. + Сохранение в файл результата и черновика изображения. + Копирование в буфер обмена картинки результата + Вставки из буфера исходной картинки
