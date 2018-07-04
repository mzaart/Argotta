# Argotta

Bridging down the barriers among employees at multinational companies, challenging the status quo of communication.

![Argotta Concept](https://i.imgur.com/UwwzqTF.png)

Argotta is a mobile application that helps you to communicate with people who speak different language. It translates the messages being sent between two users instantly. Moreover, Argotta allows you to switch to a Travel Mode where you will be able to translate text to any selected language on the spot, without the need to chat with another user. 

This repository contains the code of the backend API.

## Screen Mockups


<img src="https://lh3.googleusercontent.com/flMhdMziEMvV62SlEjZsBe8kKES7szvromakkwnyUYJOJUpRw-8WiPjbvAHOZMKOam1IDUgv1b3CUqFh5oWV-cduKTLkpFZNSfg9xX5pnexdiPRdeyzP_gjxuRU5-uei-BZCMV1q" width="219" height="374" align="left"/>
<img src="https://lh3.googleusercontent.com/hN59H8N-mEXY9b1qBwagXViZ_SOBoggnly9rMh-3jDK-uA8ggxel2tA1BF8qHxdWIN1OC4bnixUPGTlQ2DPAKpc0BOjp8fkYjqHk43gToTwP9XAvx0nZLvjVOZIbHBsk_KlyVk8v" width="219" height="374" align="left"/>
<img src="https://lh6.googleusercontent.com/7qbHMRN3SnJXdt1Ia0VJTTvw4gcfK0AyMREJr6Ju8eH_LDnflOHmtL6A1MXMdsip8bF4J7fB7oUFjnrJSFk6kZMepq91zrYEpo3b-I2fBujkvXVgf9VAz0XLQzApQXWQUC9WvGXA" width="219" height="374" align="left"/>

<img src="https://lh3.googleusercontent.com/JiMt8nzJ1R14nBR4DTpc4f1ohIXOP4J1qkOv6exLTDqZ_ubvv0WIWwSb28-qUm42uLHNcAL0_9DkaQEv0qYl2cbAtlimKbkUW1bpwQUzuI9a-IOkkv38MedT0Eg4EF-0OSmFjlSg" width="219" height="374" align="left"/>
<img src="https://lh3.googleusercontent.com/Tztk2Oqe62NPyNnimGtx2bPXrHfOrZvtKijwQtR0Uu4H7FQG10KE9iHfh3UY42QuEQ9x3VCLqfVdhQSHfWWu2gykGadEOxf-dSYptTOdOp0S4v4jt-mm7xYZKC2KkLowGVNUwMh-" width="219" height="374" align="left"/>
<img src="https://lh3.googleusercontent.com/ufosyBvmzl-byV1o23t0UuobXMTJ_99qvg21NzMK853gGq9F-7z-bn2eYzYQsdwDTD3gagqpQgwTA6AL-vZs5rxst2b62z1wDkRhPtyqiimgLEbpDFAiUtWaNlofHnj7bUlCsJmz" width="219" height="374"/>

## Key Packages in the API

- Controllers: Defines REST endpoints to communicate with the API
- Models: Contain the data classes that define the expected request format, responses, database schema and notification payloads
- Repositories: Provide a way to interact with the persistant storage
- RequestPipeline: Contains Authentication Filters, Request Validation and Logging, in addtion to error handling
- Services: Handles communication with Translation APIs, Firebase Cloud Messaging. Also, handles JWT authentication.

## Tech Stack

<img src="https://lh3.googleusercontent.com/w0L-Qn0FPfsr61UhKwIL0dC5ZCjXpuN-Jto49D0ysipNRhxal577I5-MMdAET06Ru7xtMW7zlh-doUrOQLDkK0oSuDWq3-G_9yBf32CBVNGgvAFZ8MIHG8YC0BlIMzkVcuIHtPvz"/>
