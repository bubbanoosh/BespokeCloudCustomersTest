import React, { Component } from 'react';
import { connect } from 'react-redux'
import { Link } from 'react-router-dom';
import { customerActions } from '../_actions/customer.actions';

import { Loader } from '../_components/Common'
import CustomerList from '../_components/CustomerList'

import PropTypes from 'prop-types';

class CustomerDeletePage extends Component {

    componentDidMount() {
        this.props.dispatch(customerActions.getCustomers(''));
    }

    render() {

        const { customers } = this.props

        return (
            <div>
                <nav aria-label="breadcrumb">
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item"><Link to="/">Home</Link></li>
                        <li className="breadcrumb-item active" aria-current="page">Delete Customer</li>
                    </ol>
                    </nav>
                <h2>Delete Customer</h2>

                {customers.loading ? <Loader /> : (customers.customersData.length > 0 &&
                    <form>
                        <div className="form-row">
                            <div className="form-group col-md-6">
                                <label htmlFor="firstName">Give name</label>
                                <input type="text" className="form-control" id="firstName" placeholder="Given name" />
                            </div>
                            <div className="form-group col-md-6">
                                <label htmlFor="lastName">Surname</label>
                                <input type="text" className="form-control" id="lastName" placeholder="Surname" />
                            </div>
                        </div>
                        <div className="form-group">
                            <label htmlFor="emailAddress">Email</label>
                            <input type="email" className="form-control" id="emailAddress" placeholder="name@domain.com" />
                        </div>

                        <div className="btn-group mr-2" role="group" aria-label="First group">
                            <button type="submit" className="btn btn-danger">Delete Customer</button>
                            <Link to="/" className="btn btn-secondary">
                                Cancel
                            </Link>
                        </div>
                    </form>
                )}

            </div>
        );
    }
}

CustomerDeletePage.propTypes = {
    customers: PropTypes.object.isRequired,
};

function mapStateToProps(state) {
    const { customers } = state;
    return {
        customers
    }
}

export default connect(mapStateToProps)(CustomerDeletePage)