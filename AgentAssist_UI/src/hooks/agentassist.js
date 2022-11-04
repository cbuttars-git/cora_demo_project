import { toRefs, reactive } from "vue";
import axios from "axios";
const API_URL = "https://localhost:22185/InstantAssistant"; // "http://qa-agentassist-api.usa-ed.net/InstantAssistant"; // "http://172.18.80.131:8080/InstantAssistant"; // 
class Client {
    id = -1;
    name = "";
    logo = "";
}
const state = reactive({
    error: null,
    loading: false,
    sessionId: null,
    config: null,
    query: "",
    uid: "e12345",
    responses: [],
    welcomeResponse: {
        query: "",
        url: "",
        answer: [
            {
                text: ""
            }
        ],
        feedbackId: -1,
        feedback: null
    },
    errorResponse: {
        query: "",
        url: "",
        answer: [
            {
                text: ""
            }
        ],
        feedbackId: -1,
        feedback: null
    },
    history: [],
    clients: []
});
const processOptions = (query, url, answer, options) => {
    const response = {
        query: query,
        url: url,
        answer: [],
        feedbackId: -1,
        feedback: null
    };
    const newOptions = [];
    answer = answer.replaceAll("\n", "");
    for (const [key, value] of Object.entries(options)) {
        if (answer.includes(key)) {
            answer = answer.replace(`${key}`, "<option>");
            if (typeof value === "object") {
                const newOption = processOptions(key, url, value.answer, value.options);
                newOptions.push(newOption);
            }
            else {
                newOptions.push({
                    query: key,
                    url: url,
                    answer: [{ text: value }],
                    feedbackId: -1,
                    feedback: null
                });
            }
        }
        else {
            newOptions.push({
                query: key,
                url: url,
                answer: [{ text: value }],
                feedbackId: -1,
                feedback: null
            });
        }
    }
    if (answer.includes("<option>")) {
        answer.split("<option>").forEach((text, i) => {
            response.answer.push({
                text: text,
            });
            if (newOptions[i]) {
                response.answer.push({
                    text: newOptions[i].query,
                    response: {
                        query: newOptions[i].query,
                        url: url,
                        answer: newOptions[i].answer,
                        feedbackId: -1,
                        feedback: null
                    },
                });
            }
        });
    }
    else {
        response.answer.push({
            text: answer,
        });
        newOptions.forEach((option) => {
            response.answer.push({
                text: option.query,
                response: {
                    query: option.query,
                    url: url,
                    answer: option.answer,
                    feedbackId: -1,
                    feedback: null
                },
            });
        });
    }
    if (answer.includes("Please try again") && state.errorResponse.answer[0].text !== "") {
        response.answer.push({
            text: "Suggestions",
            response: state.errorResponse,
        });
    }
    console.log("Response: ", response);
    return response;
};
export default () => {
    const resetCache = () => {
        Object.keys(localStorage).forEach(key => {
            if (key.startsWith("cache.")) {
                localStorage.removeItem(key);
            }
        });
        state.responses = [];
    };
    const getResponsesFromCache = async (client) => {
        const cacheDate = localStorage.getItem("cacheDate");
        const today = new Date().toDateString();
        if (cacheDate && cacheDate !== today) {
            resetCache();
        }
        else {
            const cache = localStorage.getItem(`cache.${client}`);
            if (cache) {
                const responses = JSON.parse(cache);
                state.responses = responses;
            }
        }
        localStorage.setItem("cacheDate", today);
    };
    const addResponse = (response) => {
        state.responses.push(response);
        localStorage.setItem(`cache.${state.config?.chatBotName}`, JSON.stringify(state.responses));
    };
    const getHistory = (client) => {
        const historyCache = localStorage.getItem(`history.${client}`);
        if (historyCache) {
            const history = JSON.parse(historyCache);
            state.history = history;
        }
    };
    const addToHistory = () => {
        if (state.history.length >= 10) {
            state.history.shift();
        }
        state.history.push(state.query);
        localStorage.setItem(`history.${state.config?.chatBotName}`, JSON.stringify(state.history));
    };
    const getClientList = async () => {
        state.loading = true;
        axios.get(`${API_URL}/ClientsInfo`, { withCredentials: true })
            .then(res => {
            state.clients = res.data;
        })
            .catch(error => {
            state.error = error.response.data;
        })
            .finally(() => {
            state.loading = false;
        });
    };
    const newSession = async (clientName) => {
        state.loading = true;
        axios.post(`${API_URL}/Session`, {
            clientName: clientName
        }, { withCredentials: true })
            .then(res => {
            state.sessionId = res.data.id;
            state.config = res.data.config;
            if (state.config?.iaConfigData.welcomeIntent !== null) {
                state.welcomeResponse = {
                    query: "",
                    url: state.config?.iaConfigData.welcomeIntent.url || "",
                    answer: [
                        {
                            text: state.config?.iaConfigData.welcomeIntent.response || ""
                        }
                    ],
                    feedbackId: -1,
                    feedback: null
                };
            }
            if (state.config?.iaConfigData.errorIntent !== null) {
                state.errorResponse = {
                    query: "",
                    url: state.config?.iaConfigData.errorIntent.url || "",
                    answer: [
                        {
                            text: state.config?.iaConfigData.errorIntent.response || ""
                        }
                    ],
                    feedbackId: -1,
                    feedback: null
                };
            }
            document.title = state.config?.chatBotName ? state.config?.chatBotName : 'Agent Assist';
        })
            .catch(error => {
            state.error = error.response.data;
            // setShowError(true);
        })
            .finally(() => {
            state.loading = false;
            (QueryRequest);
            request;
        });
        {
        }
        ;
    };
    const sendFeedback = (success, response) => {
        state.loading = true;
        axios.post(`${API_URL}/Feedback`, {
            clientId: state.config?.chatBotID,
            sessionId: state.sessionId,
            success: success,
            response: JSON.stringify(response),
            feedbackId: response.feedbackId
        }, { withCredentials: true })
            .then((res) => {
            response.feedbackId = res.data;
            response.feedback = success;
            localStorage.setItem("cache", JSON.stringify(state.responses));
        })
            .catch(error => {
            state.error = error.response.data;
        })
            .finally(() => {
            state.loading = false;
        });
    };
    const getqnaresponse = async () => {
        state.loading = true;
        addToHistory();
        try {
            const { data } = await axios.post(`${API_URL}/Query`, {
                sessionId: state.sessionId,
                query: state.query?.toLowerCase(),
                clientId: state.config?.chatBotID
            }, { withCredentials: true });
            addResponse(processOptions(data.query, data.url, data.answer, data.options));
            state.query = "";
        }
        catch (error) {
            state.error = error;
        }
        finally {
            state.loading = false;
        }
    };
    return {
        ...toRefs(state),
        getClientList,
        newSession,
        sendFeedback,
        getqnaresponse,
        resetCache,
        getResponsesFromCache,
        addResponse,
        getHistory,
        addToHistory,
    };
};
//# sourceMappingURL=agentassist.js.map