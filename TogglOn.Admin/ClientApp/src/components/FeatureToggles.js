import React, { Component } from 'react';

class FeatureToggles extends Component {
    componentWillMount() {
        this.props.requestFeatureToggles();
    }

    render() {
        return (
            <div>
                {renderFeatureToggles(this.props)}

            </div>
        );
    }
}

function renderFeatureToggles(props) {
    return (
        <table className='table'>
            <thead>
                <tr>
                    <th>Activated</th>
                    <th>Name</th>
                    <th>Namespace</th>
                    <th>Environment</th>
                </tr>
            </thead>
            <tbody>
                {props.featureToggles.length ? props.featureToggles.map(toggle =>
                    <tr key={toggle.id} className={toggle.activated ? "success" : "danger"}>
                        <td>
                            <button type='button' onClick={() => props.requestUpdateFeatureToggleActivated(toggle)} className={toggle.activated ? "btn btn-success" : "btn btn-danger"}>
                                <span className='glyphicon glyphicon-ok'></span>
                            </button>
                        </td>
                        <td>{toggle.name}</td>
                        <td>{toggle.namespace}</td>
                        <td>{toggle.environment}</td>
                    </tr>
                ) : renderMessage(props)}
            </tbody>
        </table>
    );
}

function renderMessage(props) {
    if(props.spinner.active){
        return null;
    }

    return (
        <tr>
            <td colSpan='4'>
                <div className='alert alert-warning'>
                    <strong>No featuretoggles were found!</strong>
                </div>
            </td>
        </tr>
    );
}

export default FeatureToggles;
