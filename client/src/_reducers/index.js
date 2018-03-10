import { combineReducers } from 'redux';

import { authState } from './auth.reducer';
import { registrationState } from './registration.reducer';
import { customersState } from './customers.reducer';
import { userState } from './user.reducer';
import { alert } from './alert.reducer';

const rootReducer = combineReducers({
    authState,
    registrationState,
    customersState,
    userState,
    alert,
});

export default rootReducer;