import { appConfig } from '../_helpers';
import { promiseManager } from '../_helpers';

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

    return fetch(`${appConfig.API_ROOT_URL}/customers`, requestOptions)
        .then(promiseManager.handleResponse, promiseManager.handleError);
}

function getCustomer(id) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(`${appConfig.API_ROOT_URL}/customers/${id}`, requestOptions)
        .then(promiseManager.handleResponse, promiseManager.handleError);
}

function getCustomers(searchText) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(`${appConfig.API_ROOT_URL}/customers/list/${searchText}`, requestOptions)
        .then(promiseManager.handleResponse, promiseManager.handleError);
}

function removeCustomer(id) {
    const requestOptions = {
        method: 'DELETE'
    };

    return fetch(`${appConfig.API_ROOT_URL}/customers/${id}`, requestOptions)
        .then(promiseManager.handleResponse, promiseManager.handleError);
}

function updateCustomer(customer) {
    const requestOptions = {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(customer)
    };

    return fetch(`${appConfig.API_ROOT_URL}/customers/${customer.id}`, requestOptions)
        .then(promiseManager.handleResponse, promiseManager.handleError);
}
