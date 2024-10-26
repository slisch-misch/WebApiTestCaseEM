﻿# Программа отчетности по районам

Эта программа предназначена для получения заказов и информации о районах.
Для удобства работы была добавлена документация в Swagger по ссылке: http://localhost:5143/swagger/index.html

## Основное функционирование

Программа выполняет следующие действия:

1. Выводит список всех районов
2. Позволяет пользователю выбрать интересующий район
3. Фильтрует заказы по выбранному району и указанному периоду
4. Возвращает заказы, удовлетворяющие условиям
5. Записывает логи

## Использование

1. Получите список районов (GetDistricts)
2. Выберите идентификатор района из списка районов
3. Передайте в метод GetOrders необходимые фильтры (districtId, deliveryStartFrom) 
4. Получите заказы, удовлетворяющие введённым фильтрам

## Логи и результаты

Программа автоматически логирует в консоль результаты работы запросов

## Возможные проблемы

- Неверный формат ввода данных
- Проблемы с доступом к файлам

В случае возникновения ошибок, обратитесь к выводимому сообщению об ошибке.
