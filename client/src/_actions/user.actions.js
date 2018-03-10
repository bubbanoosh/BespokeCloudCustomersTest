import { userActionTypes as actionTypes } from '../_constants';
import { userService as service } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const userActions = {
    getUsers,
    removeUser,
    updateUser,
};

function getUsers() {
    return dispatch => {
        dispatch(request());

        service.getUsers()
            .then(
                users => dispatch(success(users)),
                error => dispatch(failure(error))
            );
    };

    function request() { return { type: actionTypes.GETALL_USERS_REQUEST }; }
    function success(users) { return { type: actionTypes.GETALL_USERS_SUCCESS, users }; }
    function failure(error) { return { type: actionTypes.GETALL_USERS_FAILURE, error }; }
}

function removeUser(user) {
    return dispatch => {
        dispatch(request(user.id));

        service.removeUser(user.id)
            .then(
                () => { 
                    dispatch(success(user.id));
                    history.push('/');
                    dispatch(alertActions.success('User deleted successfully'));
                },
                error => {
                    dispatch(failure(user.id, error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(id) { return { type: actionTypes.REMOVE_USER_REQUEST, id }; }
    function success(id) { return { type: actionTypes.REMOVE_USER_SUCCESS, id }; }
    function failure(id, error) { return { type: actionTypes.REMOVE_USER_FAILURE, id, error }; }
}

function updateUser(id) {
    return dispatch => {
        dispatch(request(id));

        service.updateUser(id)
            .then(
                () => { 
                    dispatch(success());
                    history.push('/');
                    dispatch(alertActions.success('User update was successful'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(id) { return { type: actionTypes.UPDATE_USER_REQUEST, id }; }
    function success(id) { return { type: actionTypes.UPDATE_USER_SUCCESS, id }; }
    function failure(id, error) { return { type: actionTypes.UPDATE_USER_FAILURE, id, error }; }
}