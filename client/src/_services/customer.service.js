import { appConfig } from '../_helpers';

export const customerService = {
    addCustomer,
    getCustomer,
    getCustomers,
    removeCustomer,
    updateCustomer,
};

function addCustomer(customer) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(customer)
    };

    return fetch(`${appConfig.API_ROOT_URL}/customers`, requestOptions).then(handleResponse, handleError);
}

function getCustomer(id) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(`${appConfig.API_ROOT_URL}/customers/${id}`, requestOptions).then(handleResponse, handleError);
}

function getCustomers(searchText) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(`${appConfig.API_ROOT_URL}/customers/${searchText}`, requestOptions).then(handleResponse, handleError);
}

function removeCustomer(id) {
    const requestOptions = {
        method: 'DELETE'
    };

    return fetch(`${appConfig.API_ROOT_URL}/customers/${id}`, requestOptions).then(handleResponse, handleError);
}

function updateCustomer(customer) {
    const requestOptions = {
        method: 'PUT',
        headers: {'Content-Type': 'application/json' },
        body: JSON.stringify(customer)
    };

    return fetch(`${appConfig.API_ROOT_URL}/customers/${customer.id}`, requestOptions).then(handleResponse, handleError);
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

