import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Loader } from '../../_components/Common';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';

class CustomerForm extends Component {

    constructor(props) {
        super(props);

        this.state = {
            customer: {
                ...this.props.customer
            },
            loading: this.props.loading,
            operationText: this.props.operationText,
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
        if (customer.firstName && customer.lastName && customer.email) {
            this.props.dispatch(this.props.customerAction(customer));
        }
    };

    render() {
        const { customer } = this.state;
        const { loading, operationText, disabled } = this.props;
        const formState = disabled ? 'disabled' : '';
        return (
            <div>
                <h2>{operationText} Customer
                    <span className="text-secondary">
                        {(customer.firstName || customer.lastName) &&
                            `: ${customer.firstName}`}&nbsp;{customer.lastName
                        }
                    </span>
                </h2>


                <form name="form" onSubmit={this.handleSubmit}>
                    <div className="form-row">
                        <div className="form-group col-md-6">
                            <label htmlFor="firstName">Give name</label>
                            <input type="text"
                                className="form-control"
                                name="firstName"
                                id="firstName"
                                placeholder="Given name"
                                value={customer.firstName || ''}
                                onChange={this.handleChange}
                                disabled={formState}
                            />
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="lastName">Surname</label>
                            <input type="text"
                                className="form-control"
                                name="lastName"
                                id="lastName"
                                placeholder="Surname"
                                value={customer.lastName || ''}
                                onChange={this.handleChange}
                                disabled={formState}
                            />
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="emailAddress">Email</label>
                        <input type="email"
                            className="form-control"
                            name="email"
                            id="email"
                            placeholder="name@domain.com"
                            value={customer.email || ''}
                            onChange={this.handleChange}
                            disabled={formState}
                        />
                    </div>

                    <div className="btn-group mr-2" role="group" aria-label="First group">
                        <button className="btn btn-primary">{operationText} Customer</button>
                        <Link to="/" className="btn btn-danger">
                            Cancel
                        </Link>
                        {loading && <Loader />}
                    </div>
                </form>
            </div>
        );

    }
}

CustomerForm.propTypes = {
    dispatch: PropTypes.func.isRequired,
    customer: PropTypes.object.isRequired,
    loading: PropTypes.bool.isRequired,
    operationText: PropTypes.string.isRequired,
    customerAction: PropTypes.func.isRequired,
    disabled: PropTypes.bool.isRequired,
};

export default connect()(CustomerForm);