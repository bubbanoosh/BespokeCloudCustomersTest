import React, { Component } from 'react';
import { connect } from 'react-redux'
import { Link } from 'react-router-dom';
import { customerActions } from '../_actions/customer.actions';

import { Loader } from '../_components/Common'
import CustomerForm from '../_components/CustomerForm'

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
    }

    handleSubmit = (event) => {
        event.preventDefault();

        const { customer } = this.state;
        const { dispatch } = this.props;
        if (customer.firstName && customer.lastName && customer.email) {
            dispatch(customerActions.addCustomer(customer));
        }
    }

    render() {

        const { adding, customers } = this.props;
        const { customer } = this.state;

        return (
            <div>
                <nav aria-label="breadcrumb">
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item"><Link to="/">Home</Link></li>
                        <li className="breadcrumb-item active" aria-current="page">Add New Customer</li>
                    </ol>
                </nav>

                <CustomerForm customer={customer} loading={adding} operationText={'Add'} customerAction={customerActions.addCustomer} disabled={false} />

            </div>
        );
    }
}

function mapStateToProps(state) {
    const { adding } = state.customersState;
    return {
        adding
    }
}

export default connect(mapStateToProps)(CustomerAddPage)