# Spells and Battlefield

## Общее описание
Это платформер-шутер от первого лица с мини аренами на платформах. Боевая система заключается в использовании различных видов заклинаний в виде карт.


| <img src=https://github.com/KienanUnique/Spells-and-Battlefield/blob/master/Promo/sb_main_menu.gif alt="drawing" width="350"/> | <img src=https://github.com/KienanUnique/Spells-and-Battlefield/blob/master/Promo/sb_parkour.gif alt="drawing" width="350"/> |


В данный момент проект находится в процессе разработки


| <img src=https://github.com/KienanUnique/Spells-and-Battlefield/blob/master/Promo/sb_warlock_death_pickable_items.gif alt="drawing" width="350"/> | <img src=https://github.com/KienanUnique/Spells-and-Battlefield/blob/master/Promo/sb_warlock_ai_and_buff.gif alt="drawing" width="350"/> |


В данном проекте все игровые системы реализованы модульно, чтобы геймдизайнер мог удобно менять поведение разных объектов без необходимости разбираться в коде.


<img src=https://github.com/KienanUnique/Spells-and-Battlefield/blob/master/Promo/sb_warlock_ai_editor.jpg alt="drawing" height="600"/>



## Что реализовано? 
* Добавление новых заклинаний, противников и выпадаемых предметов производится без необходимости менять что-то в коде. Это достигается благодаря использованию комбинаций различных ScriptableObject-ов.
* Количество MonoBehaviour классов максимально снижено, а основная логика производится в обычных классах. MonoBehaviour классы используются в качестве контроллеров обычных классов.
* UI реализован при помощи паттерна MVP, что упрощает его поддержку и расширение.
* Инициализация контроллеров вынесена в отдельные сетап компоненты, что позволяет разделить ответственность по получению зависимостей. Также реализована система гарантирующая, что объект не будет инициализирован, пока все его зависимости не будут готовы к использованию.
* UI, многие игровые системы, префабы игроков, противников и подбираемых предметов добавляются на сцену только во время запуска игры. Благодаря этому на сцене находится минимум внутриигровых префабов, а все внутриигровые объекты гарантированно одни и те же.
* Противники и подбираемые предметы добавляются при помощи специальных зон спавна, что позволяет отложить их создание и экономить используемую память.


## Поддерживаемые платформы
* Windows


## Платформа разработки
* Unity


## Авторы
* **KienanUnique**: разработка всех игровых систем  и инструментария для геймдизайнера, настройка и поиск ассетов, верстка уровней, организация и руководство процессом разработки
    * Почта: [gizatullinakram@gmail.com](mailto:gizatullinakram@gmail.com) 
    * [GitHub](https://github.com/KienanUnique)
* **LoliesAreTheBest**: геймдизайн, сюжет, концептирование и верстка уровней
    * Почта: [kravtsovvovka@gmail.com](mailto:kravtsovvovka@gmail.com) 
    * [GitHub](https://github.com/LoliesAreTheBest)
* **MrRocks**: геймдизайн, сюжет, концептирование и верстка уровней
    * Почта: [nicerocks166@gmail.com](mailto:nicerocks166@gmail.com) 
    * [GitHub](https://github.com/MRR0CKS)
* **Hamonan**: визуальный дизайн уровней, поиск ассетов, 3д моделирование, UI, 2д арты
    * Почта: [wuillish@gmail.com](mailto:wuillish@gmail.com) 
    * [GitHub](https://github.com/Kaitiro)