import { appConfig } from '../_helpers';
import { promiseManager } from '../_helpers';

export const registrationService = {
    register,
};

function register(user) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    return fetch(`${appConfig.API_ROOT_URL}/users/register`, requestOptions)
        .then(promiseManager.handleResponse, promiseManager.handleError);
}
