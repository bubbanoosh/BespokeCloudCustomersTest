import { actionTypes as actionTypes } from '../_constants';
import { userService } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const userActions = {
    login,
    logout,
    register,
    getUsers,
    removeUser,
    updateUser,
};

function login(username, password) {
    return dispatch => {
        dispatch(request({ username }));

        userService.login(username, password)
            .then(
                user => { 
                    dispatch(success(user));
                    history.push('/');
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(user) { return { type: actionTypes.LOGIN_REQUEST, user }; }
    function success(user) { return { type: actionTypes.LOGIN_SUCCESS, user }; }
    function failure(error) { return { type: actionTypes.LOGIN_FAILURE, error }; }
}

function logout() {
    userService.logout();
    return { type: actionTypes.LOGOUT };
}

function register(user) {
    return dispatch => {
        dispatch(request(user));

        userService.register(user)
            .then(
                () => { 
                    dispatch(success());
                    history.push('/login');
                    dispatch(alertActions.success('Registration successful'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(user) { return { type: actionTypes.REGISTER_REQUEST, user }; }
    function success(user) { return { type: actionTypes.REGISTER_SUCCESS, user }; }
    function failure(error) { return { type: actionTypes.REGISTER_FAILURE, error }; }
}

function getUsers() {
    return dispatch => {
        dispatch(request());

        userService.getUsers()
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

        userService.removeUser(user.id)
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

        userService.updateUser(id)
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