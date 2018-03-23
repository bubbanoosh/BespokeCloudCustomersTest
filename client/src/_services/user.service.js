import { authHeader, appConfig } from '../_helpers';
import { promiseManager } from '../_helpers';

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
        .then(promiseManager.handleResponse, promiseManager.handleError);
}

function removeUser(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };

    return fetch(`${appConfig.API_ROOT_URL}/users/${id}`, requestOptions)
        .then(promiseManager.handleResponse, promiseManager.handleError);
}

function updateUser(user) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    return fetch(`${appConfig.API_ROOT_URL}/users/${user.id}`, requestOptions)
        .then(promiseManager.handleResponse, promiseManager.handleError);
}
