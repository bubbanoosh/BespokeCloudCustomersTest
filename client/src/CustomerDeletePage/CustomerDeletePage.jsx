import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { customerActions } from '../_actions/customer.actions';

import PropTypes from 'prop-types';

import CustomerForm from '../_components/CustomerForm';

class CustomerDeletePage extends Component {

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

        const Fragment = React.Fragment;
        const { customersState } = this.props;
        const { deleting, customer } = customersState;

        return (
            <Fragment>
                <nav aria-label="breadcrumb">
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item"><Link to="/">Home</Link></li>
                        <li className="breadcrumb-item active" aria-current="page">Delete Customer</li>
                    </ol>
                </nav>

                {customer &&
                    <CustomerForm
                        customer={customer}
                        loading={deleting}
                        operationText={'Delete'}
                        customerAction={customerActions.removeCustomer}
                        disabled={true}
                    />}

            </Fragment>
        );
    }
}

CustomerDeletePage.propTypes = {
    dispatch: PropTypes.func.isRequired,
    customersState: PropTypes.object.isRequired,
    match: PropTypes.shape({
        params: PropTypes.shape({
            id: PropTypes.node,
        }).isRequired,
    }).isRequired
};

function mapStateToProps(state) {
    const { customersState } = state;
    return {
        customersState
    };
}

const connectedCustomerDeletePage = connect(mapStateToProps)(CustomerDeletePage);
export { connectedCustomerDeletePage as CustomerDeletePage };