## Плагин для Pilot-ICE

Для конвертации атрибутов, в которых записанны денежые единицы, в словестный формат, например:
51200 в "Пятьдесят одна тысяча двести".


## Настройки

```json
[{
		"NumberAttr": "total_value",
		"StrAttr": "sum_in_words", <= сумма прописью
		"StrNumberAttr": "formatted_text" <= отформатированный текст
	}, {
		"NumberAttr": "total_value",
		"StrAttr": "sum_in_words",
		"StrNumberAttr": "formatted_text"
	}
]
```

```json
[{
		"NumberAttr": "total_value",
		"StrAttr": "sum_in_words",
		"StrNumberAttr": "formatted_text"
	}
]
```