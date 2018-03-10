import { authActionTypes as actionTypes } from '../_constants';
import { authService } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const authActions = {
    login,
    logout,
};

function login(username, password) {
    return dispatch => {
        dispatch(request({ username }));

        authService.login(username, password)
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
    authService.logout();
    return { type: actionTypes.LOGOUT };
}
