# Workout tracker app

# Table of contents

-   [Workout tracker app](#workout-tracker-app)
-   [System description](#system-description)
    -   [Purpose of the system](#purpose-of-the-system)
    -   [Functional requirements](#functional-requirements)
-   [System architecture](#system-architecture)
-   [User interface](#user-interface)
-   [API specification](#api-specification)
-   [Schemas](#schemas)

# System description

### Purpose of the system

The project aims to allow users to plan their workouts and track their progress in the application.

Once the user has signed up to the system, they can create their workouts by adding exercises to them. When a workout is performed, it is possible to fill in the results, i.e.: how many times the exercise has been performed and with which weight. It is also possible to view past workouts, create templates, and perform the workouts from them.

System roles:

-   Administrator - will be able to add and edit exercises.
-   Regular user - can create, perform, delete, view templates and workouts.
-   Premium user - can do everything a regular user can, but can create more than 3 templates.

### Functional requirements

-   Sign in to the app
-   Log in to the app
-   Log out of the app
-   View exercises and their information
-   Create a workout
-   Add exercises to the workout
-   Start the workout
-   Finish the workout
-   View workout history
-   Perform workout again from history
-   Create a workout template
-   Perform a workout from the template
-   Delete workout from history
-   Delete template
-   Edit template
-   Add a new exercise
-   Edit an exercise

# System architecture

System components:

-   Client side (front-end) - using React.js.
-   Server side (back-end) - using ASP .NET Core and PostgreSQL database

The image below shows the deployment diagram of the system. Heroku is used to host both the API and the database. Vercel is used to host the front-end application. The web application and the API are accessed via the HTTPS protocol. The API stores data in the PostgreSQL database and performs data exchange using the ORM interface.

![image](https://github.com/astijusar/WorkoutTracker/assets/60033715/78a120ab-e9a2-49e3-899d-bd11bd4ca6ce)

# User interface

### Profile:

![Profile](https://github.com/astijusar/WorkoutTracker/assets/60033715/73064202-5696-4159-bc93-1afb2fafa5bd)

### History:

![History](https://github.com/astijusar/WorkoutTracker/assets/60033715/896a729f-2021-4427-b7e2-56fb980cad20)

### Start workout:

![Workout](https://github.com/astijusar/WorkoutTracker/assets/60033715/3e23634a-4909-4bb4-bb8f-9465e4eed279)

### Exercises:

![Exercises](https://github.com/astijusar/WorkoutTracker/assets/60033715/04932385-23a4-4859-966c-1e8a56357192)

### Exercise info modal:

![Exercise info](https://github.com/astijusar/WorkoutTracker/assets/60033715/0a5dfbdf-d349-413b-a680-df5b2906439b)

### Workout creation:

![Workout start](https://github.com/astijusar/WorkoutTracker/assets/60033715/3b28cde2-a457-49d0-85b5-81bf0e8e6794)

### Add exercise to workout:

![Add exercise](https://github.com/astijusar/WorkoutTracker/assets/60033715/9842fb70-15bc-455f-abe5-85f3bd5c0259)

### Exercise creation:

![Exercise creation](https://github.com/astijusar/WorkoutTracker/assets/60033715/e01cbc91-f3dc-490b-b2f6-4d51c1011ebb)

### Exercise editing:

![exercise editing](https://github.com/astijusar/WorkoutTracker/assets/60033715/816eb7a0-a1e6-40d1-9d9e-bc35b2d8cdb7)

# API specification

<h1 id="api-auth">Auth</h1>

## post\_\_api_Auth_login

> Code samples

```javascript
const inputBody = '{
  "userName": "string",
  "password": "string"
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/Auth/login',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`POST /api/Auth/login`

_User login_

> Body parameter

```json
{
    "userName": "string",
    "password": "string"
}
```

<h3 id="post__api_auth_login-parameters">Parameters</h3>

| Name | In   | Type                                | Required | Description       |
| ---- | ---- | ----------------------------------- | -------- | ----------------- |
| body | body | [UserLoginDto](#schemauserlogindto) | false    | User login object |

<h3 id="post__api_auth_login-responses">Responses</h3>

| Status | Meaning                                                         | Description                                     | Schema |
| ------ | --------------------------------------------------------------- | ----------------------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)         | Returns access and refresh tokens               | None   |
| 401    | [Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1) | Unauthorized, username or password is incorrect | None   |

<aside class="success">
This operation does not require authentication
</aside>

## post\_\_api_Auth_register

> Code samples

```javascript
const inputBody = '{
  "userName": "string",
  "email": "string",
  "password": "string"
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/Auth/register',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`POST /api/Auth/register`

_User registration_

> Body parameter

```json
{
    "userName": "string",
    "email": "string",
    "password": "string"
}
```

<h3 id="post__api_auth_register-parameters">Parameters</h3>

| Name | In   | Type                                              | Required | Description              |
| ---- | ---- | ------------------------------------------------- | -------- | ------------------------ |
| body | body | [UserRegistrationDto](#schemauserregistrationdto) | false    | User registration object |

<h3 id="post__api_auth_register-responses">Responses</h3>

| Status | Meaning                                                                  | Description                    | Schema |
| ------ | ------------------------------------------------------------------------ | ------------------------------ | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)                  | Success                        | None   |
| 201    | [Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)             | Returns the newly created user | None   |
| 422    | [Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3) | User name already taken        | None   |

<aside class="success">
This operation does not require authentication
</aside>

## post\_\_api_Auth_refresh

> Code samples

```javascript
const inputBody = '{
  "refreshToken": "string"
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/Auth/refresh',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`POST /api/Auth/refresh`

_Get access token_

> Body parameter

```json
{
    "refreshToken": "string"
}
```

<h3 id="post__api_auth_refresh-parameters">Parameters</h3>

| Name | In   | Type                                                  | Required | Description                 |
| ---- | ---- | ----------------------------------------------------- | -------- | --------------------------- |
| body | body | [RefreshAccessTokenDto](#schemarefreshaccesstokendto) | false    | Refresh access token object |

<h3 id="post__api_auth_refresh-responses">Responses</h3>

| Status | Meaning                                                                  | Description                       | Schema |
| ------ | ------------------------------------------------------------------------ | --------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)                  | Returns access and refresh tokens | None   |
| 422    | [Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3) | Invalid token                     | None   |

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="api-exercise">Exercise</h1>

## get\_\_api_Exercise

> Code samples

```javascript
fetch("/api/Exercise", {
    method: "GET",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`GET /api/Exercise`

_Get all of the exercises_

<h3 id="get__api_exercise-parameters">Parameters</h3>

| Name           | In    | Type                              | Required | Description |
| -------------- | ----- | --------------------------------- | -------- | ----------- |
| SearchTerm     | query | string                            | false    | none        |
| MuscleGroup    | query | [MuscleGroup](#schemamusclegroup) | false    | none        |
| EquipmentType  | query | [Equipment](#schemaequipment)     | false    | none        |
| PageNumber     | query | integer(int32)                    | false    | none        |
| PageSize       | query | integer(int32)                    | false    | none        |
| SortDescending | query | boolean                           | false    | none        |

<h3 id="get__api_exercise-responses">Responses</h3>

| Status | Meaning                                                 | Description                     | Schema |
| ------ | ------------------------------------------------------- | ------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1) | Returns a list of all exercises | None   |

<aside class="success">
This operation does not require authentication
</aside>

## post\_\_api_Exercise

> Code samples

```javascript
const inputBody = '{
  "name": "string",
  "instructions": "string",
  "muscleGroup": "string",
  "equipmentType": "string"
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/Exercise',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`POST /api/Exercise`

_Create a new exercise_

> Body parameter

```json
{
    "name": "string",
    "instructions": "string",
    "muscleGroup": "string",
    "equipmentType": "string"
}
```

<h3 id="post__api_exercise-parameters">Parameters</h3>

| Name | In   | Type                                              | Required | Description              |
| ---- | ---- | ------------------------------------------------- | -------- | ------------------------ |
| body | body | [ExerciseCreationDto](#schemaexercisecreationdto) | false    | Exercise creation object |

<h3 id="post__api_exercise-responses">Responses</h3>

| Status | Meaning                                                                  | Description                                          | Schema |
| ------ | ------------------------------------------------------------------------ | ---------------------------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)                  | Success                                              | None   |
| 201    | [Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)             | Returns a newly created exercise                     | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)         | Exercise creation object sent from client is null    | None   |
| 422    | [Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3) | Invalid model state for the exercise creation object | None   |

<aside class="success">
This operation does not require authentication
</aside>

## GetExercise

<a id="opIdGetExercise"></a>

> Code samples

```javascript
fetch("/api/Exercise/{exerciseId}", {
    method: "GET",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`GET /api/Exercise/{exerciseId}`

_Get an exercise by id_

<h3 id="getexercise-parameters">Parameters</h3>

| Name       | In   | Type         | Required | Description           |
| ---------- | ---- | ------------ | -------- | --------------------- |
| exerciseId | path | string(uuid) | true     | Exercise to return id |

<h3 id="getexercise-responses">Responses</h3>

| Status | Meaning                                                        | Description               | Schema |
| ------ | -------------------------------------------------------------- | ------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)        | Returns an exercise by id | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4) | Exercise does not exist   | None   |

<aside class="success">
This operation does not require authentication
</aside>

## put\__api_Exercise_{exerciseId}

> Code samples

```javascript
const inputBody = '{
  "name": "string",
  "instructions": "string",
  "muscleGroup": "string",
  "equipmentType": "string"
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/Exercise/{exerciseId}',
{
  method: 'PUT',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`PUT /api/Exercise/{exerciseId}`

_Update exercise by id_

> Body parameter

```json
{
    "name": "string",
    "instructions": "string",
    "muscleGroup": "string",
    "equipmentType": "string"
}
```

<h3 id="put__api_exercise_{exerciseid}-parameters">Parameters</h3>

| Name       | In   | Type                                          | Required | Description               |
| ---------- | ---- | --------------------------------------------- | -------- | ------------------------- |
| exerciseId | path | string(uuid)                                  | true     | Exercise to be updated id |
| body       | body | [ExerciseUpdateDto](#schemaexerciseupdatedto) | false    | Exercise update object    |

<h3 id="put__api_exercise_{exerciseid}-responses">Responses</h3>

| Status | Meaning                                                                  | Description                                        | Schema |
| ------ | ------------------------------------------------------------------------ | -------------------------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)                  | Success                                            | None   |
| 204    | [No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5)          | No content response                                | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)         | Exercise update object is null                     | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)           | Exercise is not found                              | None   |
| 422    | [Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3) | Invalid model state for the exercise update object | None   |

<aside class="success">
This operation does not require authentication
</aside>

## patch\__api_Exercise_{exerciseId}

> Code samples

```javascript
const inputBody = '[
  {
    "operationType": 0,
    "path": "string",
    "op": "string",
    "from": "string",
    "value": null
  }
]';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/Exercise/{exerciseId}',
{
  method: 'PATCH',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`PATCH /api/Exercise/{exerciseId}`

_Partially update exercise by id_

> Body parameter

```json
[
    {
        "operationType": 0,
        "path": "string",
        "op": "string",
        "from": "string",
        "value": null
    }
]
```

<h3 id="patch__api_exercise_{exerciseid}-parameters">Parameters</h3>

| Name       | In   | Type                          | Required | Description               |
| ---------- | ---- | ----------------------------- | -------- | ------------------------- |
| exerciseId | path | string(uuid)                  | true     | Exercise to be updated id |
| body       | body | [Operation](#schemaoperation) | false    | Exercise patch object     |

<h3 id="patch__api_exercise_{exerciseid}-responses">Responses</h3>

| Status | Meaning                                                                  | Description                                       | Schema |
| ------ | ------------------------------------------------------------------------ | ------------------------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)                  | Success                                           | None   |
| 204    | [No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5)          | No content response                               | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)         | Exercise patch object is null                     | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)           | Exercise is not found                             | None   |
| 422    | [Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3) | Invalid model state for the exercise patch object | None   |

<aside class="success">
This operation does not require authentication
</aside>

## delete\__api_Exercise_{exerciseId}

> Code samples

```javascript
fetch("/api/Exercise/{exerciseId}", {
    method: "DELETE",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`DELETE /api/Exercise/{exerciseId}`

_Delete exercise by id_

<h3 id="delete__api_exercise_{exerciseid}-parameters">Parameters</h3>

| Name       | In   | Type         | Required | Description               |
| ---------- | ---- | ------------ | -------- | ------------------------- |
| exerciseId | path | string(uuid) | true     | Exercise to be deleted id |

<h3 id="delete__api_exercise_{exerciseid}-responses">Responses</h3>

| Status | Meaning                                                         | Description           | Schema |
| ------ | --------------------------------------------------------------- | --------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)         | Success               | None   |
| 204    | [No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5) | No content response   | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)  | Exercise is not found | None   |

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="api-workout">Workout</h1>

## get\_\_api_Workout

> Code samples

```javascript
fetch("/api/Workout", {
    method: "GET",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`GET /api/Workout`

_Get all of the workouts_

<h3 id="get__api_workout-parameters">Parameters</h3>

| Name           | In    | Type              | Required | Description |
| -------------- | ----- | ----------------- | -------- | ----------- |
| Template       | query | boolean           | false    | none        |
| StartFrom      | query | string(date-time) | false    | none        |
| EndTo          | query | string(date-time) | false    | none        |
| SearchTerm     | query | string            | false    | none        |
| PageNumber     | query | integer(int32)    | false    | none        |
| PageSize       | query | integer(int32)    | false    | none        |
| SortDescending | query | boolean           | false    | none        |

<h3 id="get__api_workout-responses">Responses</h3>

| Status | Meaning                                                 | Description                    | Schema |
| ------ | ------------------------------------------------------- | ------------------------------ | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1) | Returns a list of all workouts | None   |

<aside class="success">
This operation does not require authentication
</aside>

## post\_\_api_Workout

> Code samples

```javascript
const inputBody = '{
  "name": "string",
  "note": "string",
  "start": "2019-08-24T14:15:22Z",
  "end": "2019-08-24T14:15:22Z",
  "isTemplate": true
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/Workout',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`POST /api/Workout`

_Create a new workout_

> Body parameter

```json
{
    "name": "string",
    "note": "string",
    "start": "2019-08-24T14:15:22Z",
    "end": "2019-08-24T14:15:22Z",
    "isTemplate": true
}
```

<h3 id="post__api_workout-parameters">Parameters</h3>

| Name | In   | Type                                            | Required | Description             |
| ---- | ---- | ----------------------------------------------- | -------- | ----------------------- |
| body | body | [WorkoutCreationDto](#schemaworkoutcreationdto) | false    | Workout creation object |

<h3 id="post__api_workout-responses">Responses</h3>

| Status | Meaning                                                                  | Description                                         | Schema |
| ------ | ------------------------------------------------------------------------ | --------------------------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)                  | Success                                             | None   |
| 201    | [Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)             | Returns a newly created workout                     | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)         | Workout creation object sent from client is null    | None   |
| 422    | [Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3) | Invalid model state for the workout creation object | None   |

<aside class="success">
This operation does not require authentication
</aside>

## GetWorkout

<a id="opIdGetWorkout"></a>

> Code samples

```javascript
fetch("/api/Workout/{workoutId}", {
    method: "GET",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`GET /api/Workout/{workoutId}`

_Get a workout by id_

<h3 id="getworkout-parameters">Parameters</h3>

| Name      | In   | Type         | Required | Description          |
| --------- | ---- | ------------ | -------- | -------------------- |
| workoutId | path | string(uuid) | true     | Workout to return id |

<h3 id="getworkout-responses">Responses</h3>

| Status | Meaning                                                        | Description             | Schema |
| ------ | -------------------------------------------------------------- | ----------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)        | Returns a workout by id | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4) | Workout does not exist  | None   |

<aside class="success">
This operation does not require authentication
</aside>

## put\__api_Workout_{workoutId}

> Code samples

```javascript
const inputBody = '{
  "name": "string",
  "note": "string"
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/Workout/{workoutId}',
{
  method: 'PUT',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`PUT /api/Workout/{workoutId}`

_Update workout by id_

> Body parameter

```json
{
    "name": "string",
    "note": "string"
}
```

<h3 id="put__api_workout_{workoutid}-parameters">Parameters</h3>

| Name      | In   | Type                                        | Required | Description              |
| --------- | ---- | ------------------------------------------- | -------- | ------------------------ |
| workoutId | path | string(uuid)                                | true     | Workout to be updated id |
| body      | body | [WorkoutUpdateDto](#schemaworkoutupdatedto) | false    | Workout update object    |

<h3 id="put__api_workout_{workoutid}-responses">Responses</h3>

| Status | Meaning                                                                  | Description                                       | Schema |
| ------ | ------------------------------------------------------------------------ | ------------------------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)                  | Success                                           | None   |
| 204    | [No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5)          | No content response                               | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)         | Workout update object is null                     | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)           | Workout is not found                              | None   |
| 422    | [Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3) | Invalid model state for the workout update object | None   |

<aside class="success">
This operation does not require authentication
</aside>

## patch\__api_Workout_{workoutId}

> Code samples

```javascript
const inputBody = '[
  {
    "operationType": 0,
    "path": "string",
    "op": "string",
    "from": "string",
    "value": null
  }
]';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/Workout/{workoutId}',
{
  method: 'PATCH',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`PATCH /api/Workout/{workoutId}`

_Partially update workout by id_

> Body parameter

```json
[
    {
        "operationType": 0,
        "path": "string",
        "op": "string",
        "from": "string",
        "value": null
    }
]
```

<h3 id="patch__api_workout_{workoutid}-parameters">Parameters</h3>

| Name      | In   | Type                          | Required | Description              |
| --------- | ---- | ----------------------------- | -------- | ------------------------ |
| workoutId | path | string(uuid)                  | true     | Workout to be updated id |
| body      | body | [Operation](#schemaoperation) | false    | Workout patch object     |

<h3 id="patch__api_workout_{workoutid}-responses">Responses</h3>

| Status | Meaning                                                                  | Description                                      | Schema |
| ------ | ------------------------------------------------------------------------ | ------------------------------------------------ | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)                  | Success                                          | None   |
| 204    | [No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5)          | No content response                              | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)         | Workout patch object is null                     | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)           | Workout is not found                             | None   |
| 422    | [Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3) | Invalid model state for the workout patch object | None   |

<aside class="success">
This operation does not require authentication
</aside>

## delete\__api_Workout_{workoutId}

> Code samples

```javascript
fetch("/api/Workout/{workoutId}", {
    method: "DELETE",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`DELETE /api/Workout/{workoutId}`

_Delete workout by id_

<h3 id="delete__api_workout_{workoutid}-parameters">Parameters</h3>

| Name      | In   | Type         | Required | Description              |
| --------- | ---- | ------------ | -------- | ------------------------ |
| workoutId | path | string(uuid) | true     | Workout to be deleted id |

<h3 id="delete__api_workout_{workoutid}-responses">Responses</h3>

| Status | Meaning                                                         | Description          | Schema |
| ------ | --------------------------------------------------------------- | -------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)         | Success              | None   |
| 204    | [No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5) | No content response  | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)  | Workout is not found | None   |

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="api-workoutexercise">WorkoutExercise</h1>

## get\__api_workout_{workoutId}\_exercise

> Code samples

```javascript
fetch("/api/workout/{workoutId}/exercise", {
    method: "GET",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`GET /api/workout/{workoutId}/exercise`

_Get all exercises for a specific workout_

<h3 id="get__api_workout_{workoutid}_exercise-parameters">Parameters</h3>

| Name      | In   | Type         | Required | Description |
| --------- | ---- | ------------ | -------- | ----------- |
| workoutId | path | string(uuid) | true     | Workout id  |

<h3 id="get__api_workout_{workoutid}_exercise-responses">Responses</h3>

| Status | Meaning                                                 | Description                                     | Schema |
| ------ | ------------------------------------------------------- | ----------------------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1) | Returns a list of all exercises for the workout | None   |

<aside class="success">
This operation does not require authentication
</aside>

## post\__api_workout_{workoutId}\_exercise

> Code samples

```javascript
const inputBody = '{
  "exerciseId": "71ba10b8-c6bd-49fd-9742-f8dbc8ccdb47",
  "sets": [
    {
      "reps": 1,
      "weight": 0.01,
      "done": true,
      "measurementType": 0
    }
  ]
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/workout/{workoutId}/exercise',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`POST /api/workout/{workoutId}/exercise`

_Create a new exercise for a workout_

> Body parameter

```json
{
    "exerciseId": "71ba10b8-c6bd-49fd-9742-f8dbc8ccdb47",
    "sets": [
        {
            "reps": 1,
            "weight": 0.01,
            "done": true,
            "measurementType": 0
        }
    ]
}
```

<h3 id="post__api_workout_{workoutid}_exercise-parameters">Parameters</h3>

| Name      | In   | Type                                                            | Required | Description              |
| --------- | ---- | --------------------------------------------------------------- | -------- | ------------------------ |
| workoutId | path | string(uuid)                                                    | true     | Workout id               |
| body      | body | [WorkoutExerciseCreationDto](#schemaworkoutexercisecreationdto) | false    | Exercise creation object |

<h3 id="post__api_workout_{workoutid}_exercise-responses">Responses</h3>

| Status | Meaning                                                          | Description                        | Schema |
| ------ | ---------------------------------------------------------------- | ---------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)          | Success                            | None   |
| 201    | [Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)     | Returns the newly created exercise | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1) | Exercise creation object is null   | None   |

<aside class="success">
This operation does not require authentication
</aside>

## GetWorkoutExercise

<a id="opIdGetWorkoutExercise"></a>

> Code samples

```javascript
fetch("/api/workout/{workoutId}/exercise/{exerciseId}", {
    method: "GET",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`GET /api/workout/{workoutId}/exercise/{exerciseId}`

_Get a specific exercise for a workout by id_

<h3 id="getworkoutexercise-parameters">Parameters</h3>

| Name       | In   | Type         | Required | Description |
| ---------- | ---- | ------------ | -------- | ----------- |
| workoutId  | path | string(uuid) | true     | Workout id  |
| exerciseId | path | string(uuid) | true     | Exercise id |

<h3 id="getworkoutexercise-responses">Responses</h3>

| Status | Meaning                                                        | Description                          | Schema |
| ------ | -------------------------------------------------------------- | ------------------------------------ | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)        | Returns the exercise for the workout | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4) | Exercise not found                   | None   |

<aside class="success">
This operation does not require authentication
</aside>

## put\__api_workout_{workoutId}_exercise_{exerciseId}

> Code samples

```javascript
const inputBody = '{
  "order": 1,
  "sets": [
    {
      "reps": 1,
      "weight": 0.01,
      "done": true,
      "measurementType": 0
    }
  ]
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/workout/{workoutId}/exercise/{exerciseId}',
{
  method: 'PUT',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`PUT /api/workout/{workoutId}/exercise/{exerciseId}`

_Update an exercise for a workout by id_

> Body parameter

```json
{
    "order": 1,
    "sets": [
        {
            "reps": 1,
            "weight": 0.01,
            "done": true,
            "measurementType": 0
        }
    ]
}
```

<h3 id="put__api_workout_{workoutid}_exercise_{exerciseid}-parameters">Parameters</h3>

| Name       | In   | Type                                                        | Required | Description            |
| ---------- | ---- | ----------------------------------------------------------- | -------- | ---------------------- |
| workoutId  | path | string(uuid)                                                | true     | Workout id             |
| exerciseId | path | string(uuid)                                                | true     | Exercise id            |
| body       | body | [WorkoutExerciseUpdateDto](#schemaworkoutexerciseupdatedto) | false    | Exercise update object |

<h3 id="put__api_workout_{workoutid}_exercise_{exerciseid}-responses">Responses</h3>

| Status | Meaning                                                          | Description                    | Schema |
| ------ | ---------------------------------------------------------------- | ------------------------------ | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)          | Success                        | None   |
| 204    | [No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5)  | No content response            | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1) | Exercise update object is null | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)   | Exercise not found             | None   |

<aside class="success">
This operation does not require authentication
</aside>

## delete\__api_workout_{workoutId}_exercise_{exerciseId}

> Code samples

```javascript
fetch("/api/workout/{workoutId}/exercise/{exerciseId}", {
    method: "DELETE",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`DELETE /api/workout/{workoutId}/exercise/{exerciseId}`

_Delete an exercise for a workout by id_

<h3 id="delete__api_workout_{workoutid}_exercise_{exerciseid}-parameters">Parameters</h3>

| Name       | In   | Type         | Required | Description |
| ---------- | ---- | ------------ | -------- | ----------- |
| workoutId  | path | string(uuid) | true     | Workout id  |
| exerciseId | path | string(uuid) | true     | Exercise id |

<h3 id="delete__api_workout_{workoutid}_exercise_{exerciseid}-responses">Responses</h3>

| Status | Meaning                                                         | Description         | Schema |
| ------ | --------------------------------------------------------------- | ------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)         | Success             | None   |
| 204    | [No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5) | No content response | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)  | Exercise not found  | None   |

<aside class="success">
This operation does not require authentication
</aside>

## post\__api_workout_{workoutId}\_exercise_collection

> Code samples

```javascript
const inputBody = '[
  {
    "exerciseId": "71ba10b8-c6bd-49fd-9742-f8dbc8ccdb47",
    "sets": [
      {
        "reps": 1,
        "weight": 0.01,
        "done": true,
        "measurementType": 0
      }
    ]
  }
]';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/workout/{workoutId}/exercise/collection',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`POST /api/workout/{workoutId}/exercise/collection`

_Create a collection of exercises for a workout_

> Body parameter

```json
[
    {
        "exerciseId": "71ba10b8-c6bd-49fd-9742-f8dbc8ccdb47",
        "sets": [
            {
                "reps": 1,
                "weight": 0.01,
                "done": true,
                "measurementType": 0
            }
        ]
    }
]
```

<h3 id="post__api_workout_{workoutid}_exercise_collection-parameters">Parameters</h3>

| Name      | In   | Type                                                            | Required | Description               |
| --------- | ---- | --------------------------------------------------------------- | -------- | ------------------------- |
| workoutId | path | string(uuid)                                                    | true     | Workout id                |
| body      | body | [WorkoutExerciseCreationDto](#schemaworkoutexercisecreationdto) | false    | Exercise creation objects |

<h3 id="post__api_workout_{workoutid}_exercise_collection-responses">Responses</h3>

| Status | Meaning                                                          | Description                        | Schema |
| ------ | ---------------------------------------------------------------- | ---------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)          | Success                            | None   |
| 201    | [Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)     | Returns the created exercises      | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1) | Exercise creation objects are null | None   |

<aside class="success">
This operation does not require authentication
</aside>

<h1 id="api-workoutexerciseset">WorkoutExerciseSet</h1>

## get\__api_workout_{workoutId}_exercise_{exerciseId}\_set

> Code samples

```javascript
fetch("/api/workout/{workoutId}/exercise/{exerciseId}/set", {
    method: "GET",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`GET /api/workout/{workoutId}/exercise/{exerciseId}/set`

_Get all exercise sets for a specific exercise_

<h3 id="get__api_workout_{workoutid}_exercise_{exerciseid}_set-parameters">Parameters</h3>

| Name       | In   | Type         | Required | Description |
| ---------- | ---- | ------------ | -------- | ----------- |
| workoutId  | path | string(uuid) | true     | Workout id  |
| exerciseId | path | string(uuid) | true     | Exercise id |

<h3 id="get__api_workout_{workoutid}_exercise_{exerciseid}_set-responses">Responses</h3>

| Status | Meaning                                                        | Description                        | Schema |
| ------ | -------------------------------------------------------------- | ---------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)        | Returns a list of exercise sets    | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4) | Exercise or workout does not exist | None   |

<aside class="success">
This operation does not require authentication
</aside>

## post\__api_workout_{workoutId}_exercise_{exerciseId}\_set

> Code samples

```javascript
const inputBody = '{
  "reps": 1,
  "weight": 0.01,
  "done": true,
  "measurementType": 0
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/workout/{workoutId}/exercise/{exerciseId}/set',
{
  method: 'POST',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`POST /api/workout/{workoutId}/exercise/{exerciseId}/set`

_Create a new exercise set_

> Body parameter

```json
{
    "reps": 1,
    "weight": 0.01,
    "done": true,
    "measurementType": 0
}
```

<h3 id="post__api_workout_{workoutid}_exercise_{exerciseid}_set-parameters">Parameters</h3>

| Name       | In   | Type                                                                  | Required | Description                  |
| ---------- | ---- | --------------------------------------------------------------------- | -------- | ---------------------------- |
| workoutId  | path | string(uuid)                                                          | true     | Workout id                   |
| exerciseId | path | string(uuid)                                                          | true     | Exercise id                  |
| body       | body | [WorkoutExerciseSetCreationDto](#schemaworkoutexercisesetcreationdto) | false    | Exercise set creation object |

<h3 id="post__api_workout_{workoutid}_exercise_{exerciseid}_set-responses">Responses</h3>

| Status | Meaning                                                                  | Description                                              | Schema |
| ------ | ------------------------------------------------------------------------ | -------------------------------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)                  | Success                                                  | None   |
| 201    | [Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)             | Returns a newly created exercise set                     | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)         | Exercise set creation object sent from client is null    | None   |
| 422    | [Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3) | Invalid model state for the exercise set creation object | None   |

<aside class="success">
This operation does not require authentication
</aside>

## GetExerciseSet

<a id="opIdGetExerciseSet"></a>

> Code samples

```javascript
fetch("/api/workout/{workoutId}/exercise/{exerciseId}/set/{setId}", {
    method: "GET",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`GET /api/workout/{workoutId}/exercise/{exerciseId}/set/{setId}`

_Get a specific exercise set_

<h3 id="getexerciseset-parameters">Parameters</h3>

| Name       | In   | Type         | Required | Description     |
| ---------- | ---- | ------------ | -------- | --------------- |
| workoutId  | path | string(uuid) | true     | Workout id      |
| exerciseId | path | string(uuid) | true     | Exercise id     |
| setId      | path | string(uuid) | true     | Exercise set id |

<h3 id="getexerciseset-responses">Responses</h3>

| Status | Meaning                                                        | Description                                       | Schema |
| ------ | -------------------------------------------------------------- | ------------------------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)        | Returns an exercise set                           | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4) | Exercise set, exercise, or workout does not exist | None   |

<aside class="success">
This operation does not require authentication
</aside>

## put\__api_workout_{workoutId}_exercise_{exerciseId}_set_{setId}

> Code samples

```javascript
const inputBody = '{
  "reps": 1,
  "weight": 0.01,
  "done": true,
  "measurementType": 0
}';
const headers = {
  'Content-Type':'application/json-patch+json'
};

fetch('/api/workout/{workoutId}/exercise/{exerciseId}/set/{setId}',
{
  method: 'PUT',
  body: inputBody,
  headers: headers
})
.then(function(res) {
    return res.json();
}).then(function(body) {
    console.log(body);
});

```

`PUT /api/workout/{workoutId}/exercise/{exerciseId}/set/{setId}`

_Update an exercise set_

> Body parameter

```json
{
    "reps": 1,
    "weight": 0.01,
    "done": true,
    "measurementType": 0
}
```

<h3 id="put__api_workout_{workoutid}_exercise_{exerciseid}_set_{setid}-parameters">Parameters</h3>

| Name       | In   | Type                                                              | Required | Description                |
| ---------- | ---- | ----------------------------------------------------------------- | -------- | -------------------------- |
| workoutId  | path | string(uuid)                                                      | true     | Workout id                 |
| exerciseId | path | string(uuid)                                                      | true     | Exercise id                |
| setId      | path | string(uuid)                                                      | true     | Exercise set id            |
| body       | body | [WorkoutExerciseSetUpdateDto](#schemaworkoutexercisesetupdatedto) | false    | Exercise set update object |

<h3 id="put__api_workout_{workoutid}_exercise_{exerciseid}_set_{setid}-responses">Responses</h3>

| Status | Meaning                                                                  | Description                                            | Schema |
| ------ | ------------------------------------------------------------------------ | ------------------------------------------------------ | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)                  | Success                                                | None   |
| 204    | [No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5)          | No content response                                    | None   |
| 400    | [Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)         | Exercise set update object is null                     | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)           | Exercise set, exercise, or workout is not found        | None   |
| 422    | [Unprocessable Entity](https://tools.ietf.org/html/rfc2518#section-10.3) | Invalid model state for the exercise set update object | None   |

<aside class="success">
This operation does not require authentication
</aside>

## delete\__api_workout_{workoutId}_exercise_{exerciseId}_set_{setId}

> Code samples

```javascript
fetch("/api/workout/{workoutId}/exercise/{exerciseId}/set/{setId}", {
    method: "DELETE",
})
    .then(function (res) {
        return res.json();
    })
    .then(function (body) {
        console.log(body);
    });
```

`DELETE /api/workout/{workoutId}/exercise/{exerciseId}/set/{setId}`

_Delete an exercise set_

<h3 id="delete__api_workout_{workoutid}_exercise_{exerciseid}_set_{setid}-parameters">Parameters</h3>

| Name       | In   | Type         | Required | Description     |
| ---------- | ---- | ------------ | -------- | --------------- |
| workoutId  | path | string(uuid) | true     | Workout id      |
| exerciseId | path | string(uuid) | true     | Exercise id     |
| setId      | path | string(uuid) | true     | Exercise set id |

<h3 id="delete__api_workout_{workoutid}_exercise_{exerciseid}_set_{setid}-responses">Responses</h3>

| Status | Meaning                                                         | Description                                     | Schema |
| ------ | --------------------------------------------------------------- | ----------------------------------------------- | ------ |
| 200    | [OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)         | Success                                         | None   |
| 204    | [No Content](https://tools.ietf.org/html/rfc7231#section-6.3.5) | No content response                             | None   |
| 404    | [Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)  | Exercise set, exercise, or workout is not found | None   |

<aside class="success">
This operation does not require authentication
</aside>

# Schemas

<h2 id="tocS_ExerciseCreationDto">ExerciseCreationDto</h2>
<!-- backwards compatibility -->
<a id="schemaexercisecreationdto"></a>
<a id="schema_ExerciseCreationDto"></a>
<a id="tocSexercisecreationdto"></a>
<a id="tocsexercisecreationdto"></a>

```json
{
    "name": "string",
    "instructions": "string",
    "muscleGroup": "string",
    "equipmentType": "string"
}
```

### Properties

| Name          | Type        | Required | Restrictions | Description |
| ------------- | ----------- | -------- | ------------ | ----------- |
| name          | string¦null | false    | none         | none        |
| instructions  | string¦null | false    | none         | none        |
| muscleGroup   | string¦null | false    | none         | none        |
| equipmentType | string¦null | false    | none         | none        |

<h2 id="tocS_ExerciseUpdateDto">ExerciseUpdateDto</h2>
<!-- backwards compatibility -->
<a id="schemaexerciseupdatedto"></a>
<a id="schema_ExerciseUpdateDto"></a>
<a id="tocSexerciseupdatedto"></a>
<a id="tocsexerciseupdatedto"></a>

```json
{
    "name": "string",
    "instructions": "string",
    "muscleGroup": "string",
    "equipmentType": "string"
}
```

### Properties

| Name          | Type        | Required | Restrictions | Description |
| ------------- | ----------- | -------- | ------------ | ----------- |
| name          | string¦null | false    | none         | none        |
| instructions  | string¦null | false    | none         | none        |
| muscleGroup   | string¦null | false    | none         | none        |
| equipmentType | string¦null | false    | none         | none        |

<h2 id="tocS_Operation">Operation</h2>
<!-- backwards compatibility -->
<a id="schemaoperation"></a>
<a id="schema_Operation"></a>
<a id="tocSoperation"></a>
<a id="tocsoperation"></a>

```json
{
    "operationType": 0,
    "path": "string",
    "op": "string",
    "from": "string",
    "value": null
}
```

### Properties

| Name          | Type                                  | Required | Restrictions | Description |
| ------------- | ------------------------------------- | -------- | ------------ | ----------- |
| operationType | [OperationType](#schemaoperationtype) | false    | none         | none        |
| path          | string¦null                           | false    | none         | none        |
| op            | string¦null                           | false    | none         | none        |
| from          | string¦null                           | false    | none         | none        |
| value         | any                                   | false    | none         | none        |

<h2 id="tocS_RefreshAccessTokenDto">RefreshAccessTokenDto</h2>
<!-- backwards compatibility -->
<a id="schemarefreshaccesstokendto"></a>
<a id="schema_RefreshAccessTokenDto"></a>
<a id="tocSrefreshaccesstokendto"></a>
<a id="tocsrefreshaccesstokendto"></a>

```json
{
    "refreshToken": "string"
}
```

### Properties

| Name         | Type        | Required | Restrictions | Description |
| ------------ | ----------- | -------- | ------------ | ----------- |
| refreshToken | string¦null | false    | none         | none        |

<h2 id="tocS_UserLoginDto">UserLoginDto</h2>
<!-- backwards compatibility -->
<a id="schemauserlogindto"></a>
<a id="schema_UserLoginDto"></a>
<a id="tocSuserlogindto"></a>
<a id="tocsuserlogindto"></a>

```json
{
    "userName": "string",
    "password": "string"
}
```

### Properties

| Name     | Type        | Required | Restrictions | Description |
| -------- | ----------- | -------- | ------------ | ----------- |
| userName | string¦null | false    | none         | none        |
| password | string¦null | false    | none         | none        |

<h2 id="tocS_UserRegistrationDto">UserRegistrationDto</h2>
<!-- backwards compatibility -->
<a id="schemauserregistrationdto"></a>
<a id="schema_UserRegistrationDto"></a>
<a id="tocSuserregistrationdto"></a>
<a id="tocsuserregistrationdto"></a>

```json
{
    "userName": "string",
    "email": "string",
    "password": "string"
}
```

### Properties

| Name     | Type        | Required | Restrictions | Description |
| -------- | ----------- | -------- | ------------ | ----------- |
| userName | string¦null | false    | none         | none        |
| email    | string¦null | false    | none         | none        |
| password | string¦null | false    | none         | none        |

<h2 id="tocS_WorkoutCreationDto">WorkoutCreationDto</h2>
<!-- backwards compatibility -->
<a id="schemaworkoutcreationdto"></a>
<a id="schema_WorkoutCreationDto"></a>
<a id="tocSworkoutcreationdto"></a>
<a id="tocsworkoutcreationdto"></a>

```json
{
    "name": "string",
    "note": "string",
    "start": "2019-08-24T14:15:22Z",
    "end": "2019-08-24T14:15:22Z",
    "isTemplate": true
}
```

### Properties

| Name       | Type                   | Required | Restrictions | Description |
| ---------- | ---------------------- | -------- | ------------ | ----------- |
| name       | string¦null            | false    | none         | none        |
| note       | string¦null            | false    | none         | none        |
| start      | string(date-time)¦null | false    | none         | none        |
| end        | string(date-time)¦null | false    | none         | none        |
| isTemplate | boolean                | false    | none         | none        |

<h2 id="tocS_WorkoutExerciseCreationDto">WorkoutExerciseCreationDto</h2>
<!-- backwards compatibility -->
<a id="schemaworkoutexercisecreationdto"></a>
<a id="schema_WorkoutExerciseCreationDto"></a>
<a id="tocSworkoutexercisecreationdto"></a>
<a id="tocsworkoutexercisecreationdto"></a>

```json
{
    "exerciseId": "71ba10b8-c6bd-49fd-9742-f8dbc8ccdb47",
    "sets": [
        {
            "reps": 1,
            "weight": 0.01,
            "done": true,
            "measurementType": 0
        }
    ]
}
```

### Properties

| Name       | Type                                                                         | Required | Restrictions | Description |
| ---------- | ---------------------------------------------------------------------------- | -------- | ------------ | ----------- |
| exerciseId | string(uuid)                                                                 | false    | none         | none        |
| sets       | [[WorkoutExerciseSetCreationDto](#schemaworkoutexercisesetcreationdto)]¦null | false    | none         | none        |

<h2 id="tocS_WorkoutExerciseSetCreationDto">WorkoutExerciseSetCreationDto</h2>
<!-- backwards compatibility -->
<a id="schemaworkoutexercisesetcreationdto"></a>
<a id="schema_WorkoutExerciseSetCreationDto"></a>
<a id="tocSworkoutexercisesetcreationdto"></a>
<a id="tocsworkoutexercisesetcreationdto"></a>

```json
{
    "reps": 1,
    "weight": 0.01,
    "done": true,
    "measurementType": 0
}
```

### Properties

| Name            | Type                                      | Required | Restrictions | Description |
| --------------- | ----------------------------------------- | -------- | ------------ | ----------- |
| reps            | integer(int32)                            | false    | none         | none        |
| weight          | number(double)                            | false    | none         | none        |
| done            | boolean                                   | true     | none         | none        |
| measurementType | [MeasurementType](#schemameasurementtype) | false    | none         | none        |

<h2 id="tocS_WorkoutExerciseSetUpdateDto">WorkoutExerciseSetUpdateDto</h2>
<!-- backwards compatibility -->
<a id="schemaworkoutexercisesetupdatedto"></a>
<a id="schema_WorkoutExerciseSetUpdateDto"></a>
<a id="tocSworkoutexercisesetupdatedto"></a>
<a id="tocsworkoutexercisesetupdatedto"></a>

```json
{
    "reps": 1,
    "weight": 0.01,
    "done": true,
    "measurementType": 0
}
```

### Properties

| Name            | Type                                      | Required | Restrictions | Description |
| --------------- | ----------------------------------------- | -------- | ------------ | ----------- |
| reps            | integer(int32)                            | false    | none         | none        |
| weight          | number(double)                            | false    | none         | none        |
| done            | boolean                                   | true     | none         | none        |
| measurementType | [MeasurementType](#schemameasurementtype) | false    | none         | none        |

<h2 id="tocS_WorkoutExerciseUpdateDto">WorkoutExerciseUpdateDto</h2>
<!-- backwards compatibility -->
<a id="schemaworkoutexerciseupdatedto"></a>
<a id="schema_WorkoutExerciseUpdateDto"></a>
<a id="tocSworkoutexerciseupdatedto"></a>
<a id="tocsworkoutexerciseupdatedto"></a>

```json
{
    "order": 1,
    "sets": [
        {
            "reps": 1,
            "weight": 0.01,
            "done": true,
            "measurementType": 0
        }
    ]
}
```

### Properties

| Name  | Type                                                                     | Required | Restrictions | Description |
| ----- | ------------------------------------------------------------------------ | -------- | ------------ | ----------- |
| order | integer(int32)                                                           | false    | none         | none        |
| sets  | [[WorkoutExerciseSetUpdateDto](#schemaworkoutexercisesetupdatedto)]¦null | false    | none         | none        |

<h2 id="tocS_WorkoutUpdateDto">WorkoutUpdateDto</h2>
<!-- backwards compatibility -->
<a id="schemaworkoutupdatedto"></a>
<a id="schema_WorkoutUpdateDto"></a>
<a id="tocSworkoutupdatedto"></a>
<a id="tocsworkoutupdatedto"></a>

```json
{
    "name": "string",
    "note": "string"
}
```

### Properties

| Name | Type        | Required | Restrictions | Description |
| ---- | ----------- | -------- | ------------ | ----------- |
| name | string¦null | false    | none         | none        |
| note | string¦null | false    | none         | none        |
