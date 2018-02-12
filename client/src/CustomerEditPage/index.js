import React, { Component } from 'react';
import { connect } from 'react-redux'
import { Link } from 'react-router-dom';
import { customerActions } from '../_actions/customer.actions';

import PropTypes from 'prop-types';

import { Loader } from '../_components/Common'
import CustomerForm from '../_components/CustomerForm'

class CustomerEditPage extends Component {
    
    constructor(props) {
        super(props);

        this.state = {
            id: this.props.match.params.id,
            customer: {
                id: 0,
                firstName: '',
                lastName: '',
                email: ''
            }
        };
    }

    componentDidMount() {
        this.props.dispatch(customerActions.getCustomer(this.state.id));
    }

    render() {

        const { customersState } = this.props
        const { updating, customer } = customersState

        return (
            <div>
                <nav aria-label="breadcrumb">
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item"><Link to="/">Home</Link></li>
                        <li className="breadcrumb-item active" aria-current="page">Edit Customer</li>
                    </ol>
                </nav>

                {customer && <CustomerForm customer={customer} loading={updating} operationText={'Edit'} customerAction={customerActions.updateCustomer} disabled={false} />}
            </div>
        );
    }
}

CustomerEditPage.propTypes = {
    customersState: PropTypes.object.isRequired
};

function mapStateToProps(state) {
    const { customersState } = state;
    return {
        customersState
    }
}

export default connect(mapStateToProps)(CustomerEditPage)