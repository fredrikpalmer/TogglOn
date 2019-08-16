import React from 'react';

const Error = ({ errors }) => {
    if(!errors.length){
        return null;
    }
    
    return (
        <div className="alert alert-danger" role="alert">
            {errors.map(error => {
                return (
                <div key={error.message}>
                    <strong>Oops!</strong>
                    <p>{error.message}</p>
                </div>
                );
            })}
        </div>
    );
}

export default Error;
