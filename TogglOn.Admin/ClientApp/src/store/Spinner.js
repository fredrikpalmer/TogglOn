export const loadingType = 'LOADING';

const initialState = { active: false };

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === loadingType) {
        return {
            active: action.active
        };
    }

    return state;
};
