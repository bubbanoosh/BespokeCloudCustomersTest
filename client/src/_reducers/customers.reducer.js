import { customerActionTypes as actionTypes } from '../_constants';

const initialState = {
    adding: false,
    customer: {},
    customersData: [],
    deleting: false,
    error: '',
    loading: false,
    updating: false,
};

export function customersState(state = initialState, action) {
    switch (action.type) {
        case actionTypes.GET_CUSTOMER_REQUEST:
            return {
                ...state,
                updating: true
            };
        case actionTypes.GET_CUSTOMER_SUCCESS:
            return {
                ...state,
                customer: action.customer,
                updating: false
            };
        case actionTypes.GET_CUSTOMER_FAILURE:
            return {
                ...state,
                error: action.error,
                updating: false
            };
        case actionTypes.GETALL_CUSTOMERS_REQUEST:
            return {
                customersData: [],
                loading: true
            };
        case actionTypes.GETALL_CUSTOMERS_SUCCESS:
            return {
                customersData: action.customers,
                loading: false
            };
        case actionTypes.GETALL_CUSTOMERS_FAILURE:
            return {
                customersData: [],
                error: action.error,
                loading: false
            };
        case actionTypes.ADD_CUSTOMER_REQUEST:
            return {
                ...state,
                adding: true
            };
        case actionTypes.ADD_CUSTOMER_SUCCESS:
            return { ...state };
        case actionTypes.ADD_CUSTOMER_FAILURE:
            return {};
        case actionTypes.REMOVE_CUSTOMER_REQUEST:
            // add 'deleting:true' property to customer being deleted
            return {
                ...state,
                customersData: state.customersData.map(customer =>
                    customer.id === action.id
                        ? { ...customer, deleting: true }
                        : customer
                )
            };
        case actionTypes.REMOVE_CUSTOMER_SUCCESS:
            // remove deleted customer from state
            return {
                customersData: state.customersData.filter(customer => customer.id !== action.id)
            };
        case actionTypes.REMOVE_CUSTOMER_FAILURE:
            // remove 'deleting:true' property and add 'deleteError:[error]' property to customer 
            return {
                ...state,
                customersData: state.customersData.map(customer => {
                    if (customer.id === action.id) {
                        // make copy of customer without 'deleting:true' property
                        // eslint-disable-next-line no-unused-vars
                        const { deleting, ...customerCopy } = customer;
                        // return copy of customer with 'deleteError:[error]' property
                        return { ...customerCopy, deleteError: action.error };
                    }

                    return customer;
                })
            };
        case actionTypes.UPDATE_CUSTOMER_REQUEST:
            return {
                ...state,
                customer: action.customer,
                updating: true
            };
        case actionTypes.UPDATE_CUSTOMER_SUCCESS:
            return {
                ...state,
                customer: action.customer,
                updating: false
            };
        case actionTypes.UPDATE_CUSTOMER_FAILURE:
        return { 
            ...state,
            customer: action.customer,
            updating: false,
            error: action.error 
        };
    default:
            return state;
    }
}