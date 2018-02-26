import { authHeader, appConfig } from '../_helpers';

export const userService = {
    login,
    logout,
    getById,
    getUsers,
    register,
    removeUser,
    updateUser,
};

function login(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    };

    return fetch(appConfig.API_ROOT_URL + '/users/authenticate', requestOptions)
        .then(handleResponse, handleError)
        .then(user => {
            // login successful if there's a jwt token in the response
            if (user && user.token) {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
            }

            return user;
        });
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}

function getById(id) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${appConfig.API_ROOT_URL}/users/${id}`, requestOptions)
        .then(handleResponse, handleError);
}

function getUsers() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${appConfig.API_ROOT_URL}/users`, requestOptions)
        .then(handleResponse, handleError);
}

function register(user) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    return fetch(`${appConfig.API_ROOT_URL}/users/register`, requestOptions)
        .then(handleResponse, handleError);
}

function removeUser(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };

    return fetch(`${appConfig.API_ROOT_URL}/users/${id}`, requestOptions)
        .then(handleResponse, handleError);
}

function updateUser(user) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    return fetch(`${appConfig.API_ROOT_URL}/users/${user.id}`, requestOptions)
        .then(handleResponse, handleError);
}

function handleResponse(response) {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            // return json if it was returned in the response
            var contentType = response.headers.get("content-type");
            if (contentType && contentType.includes("application/json")) {
                response.json().then(json => resolve(json));
            } else {
                resolve();
            }
        } else {
            // return error message from response body
            response.text().then(text => reject(text));
        }
    });
}

function handleError(error) {
    return Promise.reject(error && error.message);
}