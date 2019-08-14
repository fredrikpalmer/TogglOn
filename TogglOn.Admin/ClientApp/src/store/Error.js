const errorType = 'ERROR';

const initialState = [];

export const addError = (message) => {
    return {
        type: errorType,
        message
    };
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === errorType) {
        return [{ message: action.message }];
    }

    if(action.type === '@@router/LOCATION_CHANGE'){
        return [];
    }

    return state;
};
