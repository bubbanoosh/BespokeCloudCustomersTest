import React from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

const CustomerItem = props => {
    return (
        <li className="list-group-item">
            <div className="btn-group mr-2" role="group" aria-label="First group">
                <Link to={`/customer/edit/${props.customer.id}`} className="btn btn-outline-primary">
                    Edit
                </Link>
                <Link to={`/customer/delete/${props.customer.id}`} className="btn btn-outline-danger">
                    Delete
                </Link>
            </div>
            {props.customer.name}
        </li>
    );
};

CustomerItem.propTypes = {
    customer: PropTypes.shape({
        id: PropTypes.node,
        name: PropTypes.node
    }).isRequired,
    customersState: PropTypes.object.isRequired,
};

export default CustomerItem;