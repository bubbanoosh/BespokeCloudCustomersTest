import { customerActionTypes as actionTypes } from '../_constants';
import { customerService } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const customerActions = {
    addCustomer,
    getCustomer,
    getCustomers,
    removeCustomer,
    updateCustomer,
};

function addCustomer(customer) {
    return dispatch => {
        dispatch(request(customer));

        customerService.addCustomer(customer)
            .then(
                () => { 
                    dispatch(success());
                    history.push('/');
                    dispatch(alertActions.success('Customer added successfully'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(customer) { return { type: actionTypes.ADD_CUSTOMER_REQUEST, customer }; }
    function success(customer) { return { type: actionTypes.ADD_CUSTOMER_SUCCESS, customer }; }
    function failure(error) { return { type: actionTypes.ADD_CUSTOMER_FAILURE, error }; }
}

function getCustomer(id) {
    return dispatch => {
        dispatch(request());

        customerService.getCustomer(id)
            .then(
                customer => dispatch(success(customer)),
                error => dispatch(failure(error))
            );
    };

    function request() { return { type: actionTypes.GET_CUSTOMER_REQUEST }; }
    function success(customer) { return { type: actionTypes.GET_CUSTOMER_SUCCESS, customer }; }
    function failure(error) { return { type: actionTypes.GET_CUSTOMER_FAILURE, error }; }
}

function getCustomers(searchText) {
    return dispatch => {
        dispatch(request());

        customerService.getCustomers(searchText)
            .then(
                customers => dispatch(success(customers)),
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request() { return { type: actionTypes.GETALL_CUSTOMERS_REQUEST }; }
    function success(customers) { return { type: actionTypes.GETALL_CUSTOMERS_SUCCESS, customers }; }
    function failure(error) { return { type: actionTypes.GETALL_CUSTOMERS_FAILURE, error }; }
}

function removeCustomer(customer) {
    return dispatch => {
        dispatch(request(customer.id));

        customerService.removeCustomer(customer.id)
            .then(
                () => { 
                    dispatch(success(customer.id));
                    history.push('/');
                    dispatch(alertActions.success('Customer deleted successfully'));
                },
                error => {
                    dispatch(failure(customer.id, error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(id) { return { type: actionTypes.REMOVE_CUSTOMER_REQUEST, id }; }
    function success(id) { return { type: actionTypes.REMOVE_CUSTOMER_SUCCESS, id }; }
    function failure(id, error) { return { type: actionTypes.REMOVE_CUSTOMER_FAILURE, id, error }; }
}

function updateCustomer(customer) {
    return dispatch => {
        dispatch(request(customer));

        customerService.updateCustomer(customer)
            .then(
                () => { 
                    dispatch(success());
                    history.push('/');
                    dispatch(alertActions.success('Customer updated successfully'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(customer) { return { type: actionTypes.UPDATE_CUSTOMER_REQUEST, customer }; }
    function success(customer) { return { type: actionTypes.UPDATE_CUSTOMER_SUCCESS, customer }; }
    function failure(error) { return { type: actionTypes.UPDATE_CUSTOMER_FAILURE, error }; }
}
