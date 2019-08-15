export const spinnerOnType = 'SPINNER_ON';
export const spinnerOffType = 'SPINNER_OFF';

const initialState = { active: false };

export const spinnerOn = () => {
    return {
        type: spinnerOnType,
    };
}

export const spinnerOff = () => {
    return {
        type: spinnerOffType,
    };
}

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === spinnerOnType) {
        return {
            active: true
        };
    }

    
    if (action.type === spinnerOffType) {
        return {
            active: false
        };
    }

    return state;
};
