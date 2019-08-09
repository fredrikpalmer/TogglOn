export const toggleLoadingType = 'TOGGLE_LOADING';
const initialState = { active: false };

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === toggleLoadingType) {
        return {
            active: action.active
        };
    }

    return state;
};
