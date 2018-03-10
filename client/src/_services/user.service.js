import { authHeader, appConfig } from '../_helpers';

export const userService = {
    getUsers,
    removeUser,
    updateUser,
};

function getUsers() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${appConfig.API_ROOT_URL}/users`, requestOptions)
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