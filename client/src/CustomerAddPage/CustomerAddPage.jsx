import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

import { customerActions } from '../_actions/customer.actions';

import CustomerForm from '../_components/CustomerForm';

class CustomerAddPage extends Component {

    constructor(props) {
        super(props);

        this.state = {
            customers: this.props,
            customer: {
                firstName: '',
                lastName: '',
                email: ''
            }
        };
    }

    handleChange = (event) => {
        const { name, value } = event.target;
        const { customer } = this.state;
        this.setState({
            customer: {
                ...customer,
                [name]: value
            }
        });
    };

    handleSubmit = (event) => {
        event.preventDefault();

        const { customer } = this.state;
        const { dispatch } = this.props;
        if (customer.firstName && customer.lastName && customer.email) {
            dispatch(customerActions.addCustomer(customer));
        }
    };

    render() {

        const Fragment = React.Fragment;
        const { adding } = this.props;
        const { customer } = this.state;

        return (
            <Fragment>
                <nav aria-label="breadcrumb">
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item"><Link to="/">Home</Link></li>
                        <li className="breadcrumb-item active" aria-current="page">Add New Customer</li>
                    </ol>
                </nav>

                <CustomerForm
                    customer={customer}
                    loading={adding}
                    operationText={'Add'}
                    customerAction={customerActions.addCustomer}
                    disabled={false}
                />

            </Fragment>
        );
    }
}

CustomerAddPage.propTypes = {
    dispatch: PropTypes.func.isRequired,
    adding: PropTypes.bool.isRequired,
};

CustomerAddPage.defaultProps = {
    adding: false,
};

function mapStateToProps(state) {
    const { adding } = state.customersState;
    return {
        adding
    };
}

const connectedCustomerAddPage = connect(mapStateToProps)(CustomerAddPage);
export { connectedCustomerAddPage as CustomerAddPage };