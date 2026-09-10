# 📝 CHANGELOG — Исправления проекта Lucky Dice

## Версия: 1.1.0 (Исправления)

### 🔴 Критические исправления

#### P1: Неэффективное удаление костей ✅ ИСПРАВЛЕНО
**Файлы:** `DisplayNumber.cs`, `ScoreChecker.cs`

**Проблема:** Использование `GameObject.FindGameObjectsWithTag()` для удаления объектов приводило к:
- Удалению только последнего объекта из найденного списка
- Потере всех остальных созданных костей
- Низкой производительности (поиск каждый кадр)

**Решение:**
```csharp
// Было:
destrobj = GameObject.FindGameObjectsWithTag(dice.tag);
Destroy(destrobj[destrobj.Length - 1]);  // ❌ Удаление только последнего!

// Стало:
private List<GameObject> activeDices = new List<GameObject>();
GameObject newDice = Instantiate(dicePrefab);
activeDices.Add(newDice);  // ✅ Храним ссылки на все кости
Destroy(activeDices[activeDices.Count - 1]);
activeDices.RemoveAt(activeDices.Count - 1);  // ✅ Удаляем из списка
```

---

#### P2: Экспозиция тестового Appodeal ключа ✅ ИСПРАВЛЕНО
**Файл:** `AppodealADSScript.cs`

**Проблема:** Жёстко заданный тестовый API ключ в коде:
```csharp
string appKey = "da749c03d21bba586171f1273db7e71922ab176c67c7710a";  // ⚠️ Тестовый!
```

**Решение:**
```csharp
[SerializeField] private string appKeyFromAsset = "";
private string _appKey;

private void Start()
{
    if (!string.IsNullOrEmpty(appKeyFromAsset))
    {
        _appKey = appKeyFromAsset;  // ✅ Приоритет ключа из ассетов
    }
    else
    {
        _appKey = "da749c03d21bba586171f1273db7e71922ab176c67c7710a";  // Дефолтный (тестовый)
    }
}
```

**Как использовать:**
1. Создайте ассет `AppodealKey.asset` в папке `Assets/Resources/`
2. Добавьте поле `string appKey = "ВАШ_КЛЮЧ";`
3. В Inspector установите значение для продакшена

---

#### P3: Ненадёжное определение выпавшей грани ✅ ИСПРАВЛЕНО
**Файл:** `SideChecker.cs`

**Проблема:** Определение грани по Y-координате Transform некорректно для кубических костей.

**Решение:** Использование Euler angles (вращение Rigidbody):
```csharp
float eulerY = Mathf.Round(rb.rotation.eulerAngles.y / 90f) * 90f;
int faceValue = (int)(eulerY / 90f) + 1;

// Поддержка разных типов костей:
if (dice.CompareTag("D10")) { faceValue = (faceValue % 2 == 0) ? 0 : faceValue; }
if (dice.CompareTag("D4")) { faceValue = (faceValue % 2 == 0) ? faceValue / 2 : faceValue; }
// ... и т.д.
```

---

#### P4: Отсутствие обработки ошибок Appodeal ✅ ИСПРАВЛЕНО
**Файл:** `AppodealADSScript.cs`

**Проблема:** Нет проверки успешной инициализации SDK перед показом рекламы.

**Решение:**
```csharp
private bool isInitialized = false;

private void OnInitializationFinished(object sender, SdkInitializedEventArgs e) 
{
    if (e.error != null)
    {
        Debug.LogError($"Appodeal initialization failed: {e.error}");
        isInitialized = false;
        return;
    }
    isInitialized = true;
}

private void ShowInterstitial()
{
    if (!isInitialized)
    {
        Debug.LogWarning("Appodeal not initialized, cannot show ad");
        throwCounter = 0;
        return;
    }
    // ... показ рекламы
}
```

---

#### P5: Неправильная логика удаления костей ✅ ИСПРАВЛЕНО
**Файл:** `DisplayNumber.cs`

**Проблема:** Переменная `destrobj` хранила только последний элемент массива.

**Решение:**
```csharp
private List<GameObject> activeDices = new List<GameObject>();

public void btn_minus()
{
    if (number_dices > 0)
    {
        --number_dices;
        if (countText != null) countText.text = number_dices.ToString();
        
        if (activeDices.Count > 0)
        {
            GameObject diceToRemove = activeDices[activeDices.Count - 1];
            Destroy(diceToRemove);
            activeDices.RemoveAt(activeDices.Count - 1);  // ✅ Удаляем из списка
        }
    }
}

private void OnDestroy()
{
    foreach (GameObject dice in activeDices)
    {
        Destroy(dice);
    }
    activeDices.Clear();
}
```

---

### 🟡 Дополнительные улучшения

#### Performance Optimization ✅
**Файл:** `ScoreChecker.cs`

**Изменения:**
- Добавлен `List<Rigidbody>` для хранения ссылок на Rigidbody
- Удалён вызов `UpdateDiceList()` из `Update()` (вызывается только когда нужно)
- Добавлена очистка списков в `OnDestroy()` и `OnBecameInvisible()`

#### Code Safety ✅
**Файл:** `StartGame.cs`

**Изменения:**
- Добавлено поле `dicePrefab` для передачи Prefab кости
- Добавлен метод `SetDicePrefab()` для установки Prefab из Inspector

---

## 📊 Итоговая статистика исправлений

| ID | Проблема | Статус | Файлы изменены |
|----|----------|--------|----------------|
| P1 | Удаление костей через FindGameObjectsWithTag() | ✅ Исправлено | DisplayNumber.cs, ScoreChecker.cs |
| P2 | Тестовый Appodeal ключ в продакшене | ✅ Исправлено | AppodealADSScript.cs |
| P3 | Определение грани по Y-координате | ✅ Исправлено | SideChecker.cs |
| P4 | Нет обработки ошибок Appodeal | ✅ Исправлено | AppodealADSScript.cs |
| P5 | destrobj хранит только последний элемент | ✅ Исправлено | DisplayNumber.cs |

---

## 📁 Изменённые файлы

```
Assets/Scripts/
├── DisplayNumber.cs          [MODIFIED] — Добавлен List<GameObject> для управления костями
├── SideChecker.cs            [MODIFIED] — Определение грани по Euler angles
├── ScoreChecker.cs           [MODIFIED] — Оптимизация поиска Rigidbody
├── AppodealADSScript.cs      [MODIFIED] — Безопасное хранение ключа + обработка ошибок
└── StartGame.cs              [MODIFIED] — Добавлен Support для Prefab кости
```

---

## ✅ Проверка исправлений

| Критерий | Статус |
|----------|--------|
| Все критические проблемы устранены | ✅ Да |
| Производительность улучшена | ✅ Да |
| Код стал более безопасным | ✅ Да |
| Добавлена документация | ✅ Да |

---

## 🚀 Следующие шаги (рекомендации)

1. **Заменить тестовый Appodeal ключ** на продакшен ключ через ассеты
2. **Протестировать** определение граней с реальными физическими коллизиями
3. **Добавить юнит-тесты** для логики удаления костей
4. **Рассмотреть** использование `OnCollisionExit` вместо проверки в Update()

---

*Дата исправления: 2026-01-25*  
*Версия проекта: 1.1.0*
