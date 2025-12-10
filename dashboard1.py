import streamlit as st 
import pandas as pd 
import plotly.express as px 
import plotly.figure_factory as ff 

# Очистка кэшей
st.cache_data.clear()
st.cache_resource.clear()

# Данные
data = pd.DataFrame({
    "Кампания": [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20],
    "Бюджет": [969957,1112612,650225,1206946,681853,1163154,1092699,1145662,1250350,1113414,
               1042455,1272740,582289,1137567,1454201,1057866,1050318,1291116,1293737,30914],
    "Показы": [9872583,443990,7055692,1362323,2071246,1694310,3200000,6957570,2938996,3654183,
               2321150,8456012,1744962,3102813,5541890,3249741,6121986,5581329,2821103,1030445],
    "Клики": [75777,8066,47392,33010,33833,20428,25335,25998,40818,35673,
              28479,26634,33421,12863,1403,32226,9826,15850,16077,2871],
    "Конверсии": [1941,129,2150,1960,1245,1009,1865,1331,2304,918,
                  888,1833,1428,464,1403,1028,486,1178,1091,157],
    "CPC": [12.80,137.94,13.72,36.57,20.14,56.93,43.12,44.09,30.63,3.12,
            36.60,47.78,17.43,88.48,36.54,32.83,106.89,81.43,80.45,10.77],
    "Доход": [4852500,322000,5375000,4900000,3112000,2522000,4662000,3327000,5760000,2295000,
              2220000,4582500,3570000,1160000,3507500,2570000,1215000,2945000,2727500,392000]
})

# Метрики
data['CTR'] = data['Клики'] / data['Показы'] * 100
data['CPA'] = data['Бюджет'] / data['Конверсии']
data['ROI'] = (data['Доход'] - data['Бюджет']) / data['Бюджет'] * 100

st.set_page_config(layout="wide")
st.title("Анализ маркетинговых кампаний")

# ===== Фильтры =====
with st.expander("Фильтры по метрикам"):
    budget_range = st.slider("Бюджет",
        int(data['Бюджет'].min()), int(data['Бюджет'].max()),
        (int(data['Бюджет'].min()), int(data['Бюджет'].max()))
    )
    cpc_range = st.slider("CPC",
        float(data['CPC'].min()), float(data['CPC'].max()),
        (float(data['CPC'].min()), float(data['CPC'].max()))
    )
    ctr_range = st.slider("CTR (%)",
        float(data['CTR'].min()), float(data['CTR'].max()),
        (float(data['CTR'].min()), float(data['CTR'].max()))
    )
    clicks_range = st.slider("Клики",
        int(data['Клики'].min()), int(data['Клики'].max()),
        (int(data['Клики'].min()), int(data['Клики'].max()))
    )
    conv_range = st.slider("Конверсии",
        int(data['Конверсии'].min()), int(data['Конверсии'].max()),
        (int(data['Конверсии'].min()), int(data['Конверсии'].max()))
    )

    filtered_data = data[
        (data['Бюджет'] >= budget_range[0]) & (data['Бюджет'] <= budget_range[1]) &
        (data['CPC'] >= cpc_range[0]) & (data['CPC'] <= cpc_range[1]) &
        (data['CTR'] >= ctr_range[0]) & (data['CTR'] <= ctr_range[1]) &
        (data['Клики'] >= clicks_range[0]) & (data['Клики'] <= clicks_range[1]) &
        (data['Конверсии'] >= conv_range[0]) & (data['Конверсии'] <= conv_range[1])
    ]

# ===== 2x2 сетка =====
row1_col1, row1_col2 = st.columns(2)
row2_col1, row2_col2 = st.columns(2)

# Таблица
with row1_col1:
    st.subheader("Таблица кампаний")
    st.dataframe(filtered_data, height=250)

# ROI
with row1_col2:
    st.subheader("ROI по кампаниям")
    fig_roi = px.bar(
        filtered_data, x='Кампания', y='ROI', color='ROI',
        color_continuous_scale='Viridis',
        labels={'ROI':'ROI (%)'},
        hover_data=['Бюджет','Клики','Конверсии','CPA']
    )
    st.plotly_chart(fig_roi, use_container_width=True, height=250)

# Scatter «Клики vs Конверсии»
with row2_col1:
    st.subheader("Клики vs Конверсии")
    fig_scatter = px.scatter(
        filtered_data, x='Клики', y='Конверсии',
        size='Бюджет', color='CTR',
        color_continuous_scale='Bluered',
        hover_data=['Кампания','Бюджет','CPC','CPA','ROI']
    )
    st.plotly_chart(fig_scatter, use_container_width=True, height=250)

# CPC и CPA гистограмма
with row2_col2:
    st.subheader("Распределение CPC и CPA")
    fig_cpc = px.histogram(filtered_data, x='CPC', nbins=10, labels={'CPC':'CPC (₽)'}, opacity=0.6)
    fig_cpa = px.histogram(filtered_data, x='CPA', nbins=10, labels={'CPA':'CPA (₽)'}, opacity=0.6)
    fig_cpc.add_traces(fig_cpa.data)
    st.plotly_chart(fig_cpc, use_container_width=True, height=250)

# Тепловая карта корреляций
st.subheader("Тепловая карта корреляций")
corr = filtered_data[['Бюджет','CPC','CTR','Клики','Конверсии','Доход','CPA','ROI']].corr()
fig_heatmap = ff.create_annotated_heatmap(
    z=corr.values,
    x=list(corr.columns),
    y=list(corr.columns),
    colorscale='Viridis',
    showscale=True
)
st.plotly_chart(fig_heatmap, use_container_width=True, height=300)
