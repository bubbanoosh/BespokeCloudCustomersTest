import React, { Component } from 'react';
import { connect } from 'react-redux'
import { Link } from 'react-router-dom';
import { customerActions } from '../_actions/customer.actions';

import { Loader } from '../_components/Common'

class CustomerAddPage extends Component {

    componentDidMount() {
        //this.props.dispatch(customerActions.getCustomers(''));

    }

    constructor(props) {
        super(props);

        this.state = {
            customers: this.props,
            customer: {
                firstName: '',
                lastName: '',
                email: ''
            },
            submitted: false
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        const { name, value } = event.target;
        const { customer } = this.state;
        this.setState({
            customer: {
                ...customer,
                [name]: value
            }
        });
    }

    handleSubmit(event) {
        event.preventDefault();

        this.setState({ submitted: true });
        const { customer } = this.state;
        const { dispatch } = this.props;
        if (customer.firstName && customer.lastName && customer.email) {
            dispatch(customerActions.addCustomer(customer));
        }
    }


    render() {

        const { adding, customers } = this.props;
        const { customer, submitted } = this.state;

        return (
            <div>
                <nav aria-label="breadcrumb">
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item"><Link to="/">Home</Link></li>
                        <li className="breadcrumb-item active" aria-current="page">Add New Customer</li>
                    </ol>
                </nav>
                <h2>Add New Customer{(customer.firstName || customer.lastName) && `: ${customer.firstName}`}&nbsp;{customer.lastName}</h2>

                <form name="form" onSubmit={this.handleSubmit}>
                    <div className="form-row">
                        <div className="form-group col-md-6">
                            <label htmlFor="firstName">Give name</label>
                            <input type="text" className="form-control" name="firstName" id="firstName" 
                                placeholder="Given name" value={customer.firstName} onChange={this.handleChange} required 
                            />
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="lastName">Surname</label>
                            <input type="text" className="form-control" name="lastName" id="lastName"
                                placeholder="Surname" value={customer.lastName} onChange={this.handleChange} required 
                            />
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="emailAddress">Email</label>
                        <input type="email" className="form-control" name="email" id="email" 
                            placeholder="name@domain.com" value={customer.email} onChange={this.handleChange} required 
                        />
                    </div>

                    <div className="btn-group mr-2" role="group" aria-label="First group">
                        <button className="btn btn-primary">Add Customer</button>
                        <Link to="/" className="btn btn-danger">
                            Cancel
                        </Link>
                        {adding && <Loader />}
                    </div>
                </form>

            </div>
        );
    }
}

function mapStateToProps(state) {
    const { adding } = state.customers;
    return {
        adding
    }
}

export default connect(mapStateToProps)(CustomerAddPage)