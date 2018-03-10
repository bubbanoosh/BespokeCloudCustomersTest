import { registrationActionTypes as actionTypes } from '../_constants';
import { registrationService as service } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const registrationActions = {
    register,
};

function register(user) {
    return dispatch => {
        dispatch(request(user));

        service.register(user)
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
