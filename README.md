# Car-Companion

Car-companion is an app to menage your car and its expenses. You can also share car or see charts, figures and statistics about car. 

## Used technologies 
.net core 3.1, automapper etc

## Installation

Use the [docker and docker-compose](https://www.docker.com/) to run frontend and backend of the application.

run this command in main repo folder:
```bash
docker-compose up -d
```

## Usage

When containers are up, you might to wait a while to apply migrations and some other necessary stuff to get application ready for work.
Now you can access the application, backend endpoint in swagger on http://localhost:8080 and frontend on http://localhost:4200

## Functionalities

|                     |                          | Backend        | Frontend       |
|     :---:           |     :---                 |     :---:      | :---:          |
| Auhorization        | Registration             | ✔️             | ❌            |
|                     | JWT AccessToken          | ✔️             | ✔️            |
|                     | Refreshing Token         | ✔️             | ❌            |
|                     | Facebook Auth            | ✔️             | ❌            |
|                     | Logout                   | ✔️             | ❌            |
|                     | RefreshToken             | ✔️             | ❌            |
| Car management      | Create car               | ✔️             | ❌            |
|                     | Get all usercars         | ✔️             | ✔️            |
|                     | Get specific car         | ✔️             | ✔️            |
|                     | Edit car                 | ✔️             | ❌            |
|                     | Delete car               | ✔️             | ❌            |
| Expense management  | Get all expenses         | ✔️             | ❌            |
|                     | Get specific expense     | ✔️             | ❌            |
|                     | Add expense              | ✔️             | ❌            |
|                     | Edit expense             | ✔️             | ❌            |
|                     | Delete expense           | ✔️             | ❌            |
| Car sharing         | Create share key         | ✔️             | ❌            |
|                     | Use share key            | ✔️             | ❌            |
|                     | Roles - permmisions      | ❌             | ✔️            |
| Logs                | Get all logs (superuser) | ⚠️             | ❌            |
|                     | Get my logs (user)       | ⚠️             | ❌            |
| Summaries           | Get summaries            | ✔️             | ➖            |
|                     | Charts                   | ➖             | ❌            |
|                     | Figures                  | ➖             | ❌            |

✔️ - work 
❌ - doesn't work 
⚠️ - work with troubles 
➖ - doesn't involve 

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)
